using UnityEngine;

//调用时自动生成对象的继承Mono的单例基类，派生类不用拖到游戏场景中
public class SingletonMonoAuto<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject temp = new GameObject(typeof(T).ToString());
                DontDestroyOnLoad(temp);
                instance = temp.AddComponent<T>();
            }
            return instance;
        }
    }
}
