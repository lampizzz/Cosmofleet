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
        // Проверяем, назначено ли поле textRoomName
        if (textRoomName == null)
        {
            Debug.LogError("Поле textRoomName не назначено в инспекторе! Проверьте, чтобы оно было привязано.");
            return;
        }

        // Проверяем, заполнено ли поле textRoomName
        if (string.IsNullOrWhiteSpace(textRoomName.text))
        {
            Debug.LogError("Название комнаты не может быть пустым. Введите название комнаты.");
            return;
        }

        // Создаем комнату с указанным именем
        PhotonNetwork.CreateRoom(textRoomName.text, new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
        Debug.Log($"Комната \"{textRoomName.text}\" успешно создана!");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void JoinRoom(string roomName)
    {
        // Проверяем, указано ли имя комнаты
        if (string.IsNullOrWhiteSpace(roomName))
        {
            Debug.LogError("Название комнаты для подключения не может быть пустым.");
            return;
        }

        // Присоединяемся к комнате
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log($"Подключение к комнате \"{roomName}\"...");
    }

    public void LeaveRoom()
    {
        // Оставляем комнату
        PhotonNetwork.LeaveRoom();
        Debug.Log("Вы покинули комнату.");
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}