using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This needs to be tested in practise! In theory, it works fine :)
public class Hub : MonoBehaviour
{
    private static IDictionary<Type, object> _instances;

    public static void Init()
    {
        _instances = new Dictionary<Type, object>();
    }

    //If you want Hub to be initialized on Scene Load
    void Awake()
    {
        Init();
        if (Register<EventHub>() == null)
        {
            Register(new EventHub());
        }

        //You may define your globals either here or in a bootstrap game object of your choice!
        //E.g.
        // Register<TestAudioControl>();
        // Hub.Get<TestAudioControl>().PlayDefaultMusic();
    }

    public static T Get<T>()
    {
        return (T)_instances[typeof(T)];
    }

    public static void Register<T>(T obj)
    {
        _instances[typeof(T)] = obj;
    }

    public static T Register<T>() where T : UnityEngine.Object
    {
        var obj = FindObjectOfType<T>();
        if (obj != null)
            _instances[typeof(T)] = obj;

        return obj;
    }
}
