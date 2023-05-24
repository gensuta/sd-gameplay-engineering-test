using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that easily turns a script into a Singleton
/// </summary>
/// <typeparam name="T">the class we want to turn into a Singleton</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if(Instance == null) // if we don't have our Instance set, this is our Instance
        {
            _instance = this as T;
        }
        else // if we have an Instance...
        {
            if (Instance != this as T) // and this isn't our Instance...
            {
                Destroy(gameObject); // destroy this gameObject. We don't need two of them!
            }
        }
    }
}