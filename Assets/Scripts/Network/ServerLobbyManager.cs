using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public override void OnLeftLobby()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
