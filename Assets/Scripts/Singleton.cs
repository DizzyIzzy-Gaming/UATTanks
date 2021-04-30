﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    public bool IsInitialized
	{
		get
		{
            return instance != null;
		}
    }

    public int score;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if (IsInitialized)
        {
            Debug.LogError("[Singleton] Tried to create a second instance of game manager");
            GameObject.Destroy(this.gameObject);

        }
        else
        {
            instance = (T) this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    protected virtual void OnDestroy()
	{
        instance = null;
	}
}
