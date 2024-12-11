using System.Collections;
using Photon.Pun;
using UnityEngine;

public class ConenctLobby : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        StartCoroutine(JoinLobbyWithDelay());
    }

    private IEnumerator JoinLobbyWithDelay()
    {
        yield return new WaitForSeconds(1f); // Задержка в 1 секунду
        PhotonNetwork.JoinLobby();
        Debug.Log("Joined lobby after 1 second delay");
    }
}
