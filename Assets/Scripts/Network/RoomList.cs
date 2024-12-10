using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Photon.Pun;
using Photon.Realtime;
using TMPro; // Подключаем библиотеку TMP
using UnityEngine;

public class RoomList : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] TMP_InputField searchInputField; // Поле ввода для поиска
    public GameObject[] AllRooms;
    private List<RoomInfo> currentRoomList = new List<RoomInfo>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Сохраняем текущий список комнат
        currentRoomList = roomList;

        // Обновляем комнаты без фильтрации
        UpdateRoomDisplay(roomList);
    }

    public void OnSearchInputChanged()
    {
        string searchPattern = searchInputField.text;

        if (string.IsNullOrWhiteSpace(searchPattern))
        {
            // Если текст пустой, показываем все комнаты
            UpdateRoomDisplay(currentRoomList);
        }
        else
        {
            try
            {
                // Компилируем регулярное выражение
                Regex regex = new Regex(searchPattern, RegexOptions.IgnoreCase);

                // Фильтруем комнаты
                var filteredRooms = currentRoomList.FindAll(room => regex.IsMatch(room.Name));
                UpdateRoomDisplay(filteredRooms);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка в регулярном выражении: {ex.Message}");
            }
        }
    }

    private void UpdateRoomDisplay(List<RoomInfo> roomList)
    {
        // Удаляем старые комнаты
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
