using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class AlreadyConnected : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text connectionText; // Текст, который отображает статус соединения
    [SerializeField] Button startBtn; // Кнопка "Start", которая будет активирована

    // Стартовое подключение
    void Start()
    {
        // Если игрок уже подключен к Photon
        if (PhotonNetwork.IsConnected)
        {
            // Обновляем UI для уже подключенного клиента
            OnConnectedToMaster();
        }
        else
        {
            // Подключаемся, если еще не подключены
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Этот метод срабатывает, когда клиент подключен к мастер-серверу Photon
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");

        // Если текст "Соединение" существует, убираем его
        if (connectionText != null)
        {
            connectionText.text = ""; // Очищаем текст
        }

        // Делаем кнопку "Start" интерактивной
        if (startBtn != null)
        {
            startBtn.interactable = true; // Активируем кнопку
        }

        AddBlinkingEffectToButtonText();
    }
    
    private void AddBlinkingEffectToButtonText()
    {
        // Получаем компонент TMP_Text, который отвечает за текст кнопки
        TMP_Text buttonText = startBtn.GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            // Добавляем компонент BlinkingText к тексту кнопки
            BlinkingText blinkingText = buttonText.gameObject.AddComponent<BlinkingText>();

            // Устанавливаем начальные параметры для мигания (можно настроить скорость и цвета)
            blinkingText.startColor = Color.black;
            blinkingText.targetColor = Color.red;
            blinkingText.blinkSpeed = 1.0f;
        }
        else
        {
            Debug.Log("Текст кнопки не найден!");
        }
    }
}