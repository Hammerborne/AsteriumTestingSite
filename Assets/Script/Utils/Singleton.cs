using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*public abstract class Singleton<ClassType> where ClassType : new()
{
    static Singleton()
    {
    }

    private static readonly ClassType instance = new ClassType();

    public static ClassType Instance
    {
        get
        {
        return instance;
        }
    }
}*/
/*public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    private Singleton()
    {   }
    public static T Instance 
    {
        get
        {
            if (Equals(_instance, default(T)))
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}*/

// Generic abstract base class
public abstract class Singleton<T>
{
    private static readonly object instanceLock = new object();
    private static T instance; // Derived class instance

    // Protected constructor accessible from derived class
    protected Singleton()
    {
    }

    // Returns the singleton instance of the derived class
    public static T Instance {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = (T)Activator.CreateInstance(typeof(T), true);
                }
                return instance;
            }
        }
    }

    public static void Clear()
    {
        lock (instanceLock)
        {
            instance = default(T); // Reset the instance to null
        }
    }
}