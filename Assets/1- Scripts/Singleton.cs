using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{ 
    static T instance;

    public T GetInstance()
    {
        return instance;
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
