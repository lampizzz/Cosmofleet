using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomList : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject roomPrefab;
    public GameObject[] AllRooms;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < AllRooms.Length; i++)
        {
            if (AllRooms[i] != null)
            {
                Destroy(AllRooms[i]);
            }
        }

        AllRooms = new GameObject[roomList.Count];
        
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                GameObject room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
        
                if (room == null)
                {
                    Debug.LogError("Failed to instantiate roomPrefab");
                    continue;
                }
        
                Room roomComponent = room.GetComponent<Room>();
                if (roomComponent == null)
                {
                    Debug.LogError("Room component not found on roomPrefab");
                    continue;
                }

                if (roomComponent.roomName == null)
                {
                    Debug.LogError("roomName field is not assigned in Room script");
                    continue;
                }

                if (roomList[i].PlayerCount == roomList[i].MaxPlayers)
                {
                    roomComponent.connectButton.interactable = false;
                }
                
                roomComponent.roomName.text = roomList[i].Name;
                roomComponent.capacityText.text = $"{roomList[i].PlayerCount}/{roomList[i].MaxPlayers}";
                AllRooms[i] = room;
            }
        }

    }
}
