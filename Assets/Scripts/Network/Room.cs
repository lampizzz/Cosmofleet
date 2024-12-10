using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public TextMeshProUGUI capacityText;
    public TextMeshProUGUI roomName;
    public Button connectButton;

    public void JoinRoom()
    {
        Debug.Log(roomName.text);
        GameObject.Find("LobbyNetwork").GetComponent<ServerRoomManager>().JoinRoom(roomName.text);
    }
}
