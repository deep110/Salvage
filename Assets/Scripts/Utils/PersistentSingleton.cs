﻿using UnityEngine;

/*
* this object has global access and 
* persist across scenes.
*/

public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour {

	private static T _instance;

    public static T Instance { get { return _instance; } }


    private void Awake() {

        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this as T;
            DontDestroyOnLoad (this);
        }
    }
}
