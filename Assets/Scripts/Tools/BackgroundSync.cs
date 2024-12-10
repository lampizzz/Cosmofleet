using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSync : MonoBehaviour
{
    public static BackgroundSync Instance { get; private set;}

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}
