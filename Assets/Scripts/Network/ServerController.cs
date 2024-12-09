using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerController : MonoBehaviourPunCallbacks
{
    public static ServerController Instance { get; private set;}

    private void Awake()
    {
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

    private void Start() 
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon connected");
    }
}
