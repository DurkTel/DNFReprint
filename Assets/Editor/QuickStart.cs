using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public class QuickStart : ScriptableObject
{
    static string m_LaunchGameTag = "QuickMenuKey_LaunchGameTag";
    static string m_LoadModeABTag = "QuickMenuKey_LoadModeABTag";


    static QuickStart()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (EditorPrefs.GetBool(m_LaunchGameTag))
            {
                EditorApplication.update += Update;
            }
        }

    }

    static void Update()
    {
        if (EditorApplication.isPlaying)
        {
            EditorPrefs.DeleteKey(m_LaunchGameTag);
            EditorApplication.update -= Update;
            CreateLaunchScene();
        }
    }

    static void CreateLaunchScene()
    {
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.CreateScene("LaunchGame");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
        System.Reflection.Assembly Assembly = System.Reflection.Assembly.Load("Assembly-CSharp");
        System.Type type = Assembly.GetType("Launcher");
        GameObject game = new GameObject("Game", type);
        DontDestroyOnLoad(game);
    }

    [MenuItem("Game/Launch #F5", false, 50)]
    static void LaunchGame()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        //本地资源模式 清点本地资源
        //AssetManifest.RefreshAssetsManifest();

        EditorPrefs.SetBool(m_LaunchGameTag, true);
        EditorPrefs.SetBool(m_LoadModeABTag, false);
        EditorApplication.isPlaying = true;

    }


    [MenuItem("Game/Launch  (AB包模式) #F6", false, 60)]
    static void LaunchGameAB()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        EditorPrefs.SetBool(m_LaunchGameTag, true);
        EditorPrefs.SetBool(m_LoadModeABTag, true);
        EditorApplication.isPlaying = true;
    }

}
