using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T foundInstance = FindFirstObjectByType<T>();
                if (foundInstance != null)
                    instance = foundInstance;
                else
                    new GameObject().AddComponent<T>().name = "Singleton_" + typeof(T).ToString();
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance != null && instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.GetComponent<T>();
        }
    }
}

public class SingletonBehavior<T> : MonoBehaviour where T : SingletonBehavior<T>
{
    public static T Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else
        {
            Destroy(this);
        }
    }
}