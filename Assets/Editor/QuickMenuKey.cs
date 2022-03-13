using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class QuickMenuKey : ScriptableObject
{
    static string m_LaunchGameTag = "QuickMenuKey_LaunchGameTag";

    static QuickMenuKey()
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

        EditorPrefs.SetBool(m_LaunchGameTag, true);
        EditorApplication.isPlaying = true;
    }

}
