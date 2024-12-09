using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI textRoomName;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(textRoomName.text, new RoomOptions() {MaxPlayers = 2, IsVisible = true, IsOpen = true}, TypedLobby.Default, null);
        Debug.Log($"The room \"{textRoomName.text}\" was created");
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Client has joined the room");
    }
}
