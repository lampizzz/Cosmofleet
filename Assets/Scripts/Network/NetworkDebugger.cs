using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkDebugger : MonoBehaviourPunCallbacks
{
    public static NetworkDebugger Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Client has joined the lobby");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Client has left the lobby");
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Client has joined the room");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Client has left the room");
    }
}
