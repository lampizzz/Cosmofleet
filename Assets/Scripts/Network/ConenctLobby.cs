using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ConenctLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button createRoomBtn;

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

    public override void OnJoinedLobby()
    {
        backBtn.interactable = true;
        createRoomBtn.interactable = true;
    }
}
