using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ServerLobbyManager : MonoBehaviourPunCallbacks
{
    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Client connected to lobby");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Client left lobby");
    }
}
