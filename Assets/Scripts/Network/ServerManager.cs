using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviourPunCallbacks
{
    private static ServerManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        Debug.Log("Photon connected");
        PhotonNetwork.ConnectUsingSettings();
    }
}
