using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class LaunchUpdate : MonoBehaviour
{

    private string m_localVersion;

    private string m_netVersion;

    private List<string> m_downloadList = new List<string>();

    public Action updateComplete;

    void Start()
    {
        StartCoroutine(HotUpdateSetp());
    }


    private IEnumerator HotUpdateSetp()
    {
        yield return GetLocalVersion();

        yield return GetNetVersion();

        yield return CheckUpdate();
    }

    private IEnumerator GetLocalVersion()
    {
        string filePath = AssetDefine.localDataPath + "version.txt";

        if (File.Exists(filePath))
        {
            string str = File.ReadAllText(filePath);
            string[] data = str.Split('|');
            m_localVersion = data[1];
            //print("本地版本号:" + m_localVersion);
        }
        else
        {
            //第一次装包 将首包的数据复制到可读可写的文件夹
            Directory.CreateDirectory(AssetDefine.localDataPath);
            print("首次安装");
            string[] files = Directory.GetFiles(Application.streamingAssetsPath);
            for (int i = 0; i < files.Length; i++)
            {
                if (Path.GetExtension(files[i]) != ".meta")
                {
                    string newPath = Path.Combine(AssetDefine.localDataPath, Path.GetFileName(files[i]));
                    File.Copy(files[i], newPath);
                }
            }

            yield return GetLocalVersion();
        }
    }

    private IEnumerator GetNetVersion()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(AssetDefine.netServerPath + "version.txt"))
        {
            yield return webRequest.SendWebRequest();
            string str = webRequest.downloadHandler.text;
            string[] data = str.Split('|');
            m_netVersion = data[1];
            //print("服务器版本号:" + m_netVersion);
        }
    }


    private IEnumerator CheckUpdate()
    {
        //版本号不同
        bool update = m_localVersion != m_netVersion;

        yield return GetDownLoadList(update);

        if (m_downloadList.Count > 0)
        {
            print(update ? "有可用更新，开始下载" : "文件异常，开始修复");
            DownLoader();
        }
        else
        {
            print("无需更新");
            updateComplete?.Invoke();
        }

    }

    private IEnumerator GetDownLoadList(bool update)
    {
        //获取资源地址的文件列表
        using (UnityWebRequest webRequest = UnityWebRequest.Get(AssetDefine.netServerPath + "file.txt"))
        {
            yield return webRequest.SendWebRequest();
            string str = webRequest.downloadHandler.text;
            string[] lines = str.Split('\n');
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] data = line.Split('|');

                    bool needDown = false;

                    if (File.Exists(AssetDefine.localDataPath + data[0]))
                    {
                        //如果存在此文件，比较当前文件md5与资源服务器文件的md5是否一致
                        string md5 = AssetUtility.GetMD5(AssetDefine.localDataPath + data[0]);
                        if (md5 != data[1])
                            needDown = true;
                    }
                    else
                    {
                        //不存在加入下载清单
                        needDown = true;
                    }

                    if (needDown)
                        m_downloadList.Add(AssetDefine.netServerPath + data[0]);
                }
            }
            //更新版本文件
            if (update)
                m_downloadList.Add(AssetDefine.netServerPath + "version.txt");
        }
    }

    private void DownLoader()
    {
        if (m_downloadList.Count > 0)
        {
            WebClient client = new WebClient();
            string name = m_downloadList[0].Substring(m_downloadList[0].LastIndexOf('/'));
            if (File.Exists(AssetDefine.localDataPath + name))
                File.Delete(AssetDefine.localDataPath + name);

            client.DownloadFileAsync(new System.Uri(m_downloadList[0]), AssetDefine.localDataPath + name);
            client.DownloadProgressChanged += (p, a) => { print(name + "下载进度:" + a.ProgressPercentage); };
            client.DownloadFileCompleted += (p, a) => { 
                m_downloadList.RemoveAt(0);
                DownLoader();
            };
        }
        else
            updateComplete?.Invoke();

    }

}
