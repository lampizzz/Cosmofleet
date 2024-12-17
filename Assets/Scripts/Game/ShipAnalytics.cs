using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Game
{
    public class ShipAnalytics : MonoBehaviour
{
    [SerializeField] TMP_Text[] shipCount;
    [SerializeField] Button[] shipBtn;
    [SerializeField] Button readyBtn;
    [SerializeField] TMP_Text statusText; // Новый текстовый элемент для статуса
    
    [SerializeField] private LocalizedString localizedPlaceShips;    // Локализованная строка "Разместите ваши корабли"
    [SerializeField] private LocalizedString localizedConfirmReady;  // Локализованная строка "Подтвердите размещение кораблей"
    [SerializeField] private LocalizedString localizedWaitingOpponent;  // Локализованная строка "Ждем соперника"
    [SerializeField] private LocalizedString localizedWaitingToStartOpponent;  // Локализованная строка "Ожидаем соперника"
    [SerializeField] private LocalizedString localizedYourTurn;      // Локализованная строка "Ваш ход"
    [SerializeField] private LocalizedString localizedOpponentTurn;  // Локализованная строка "Ход соперника"


    public BattlePlayer player;

    private void Start()
    {
        GameManager gameManager = gameObject.GetComponent<GameManager>();
        player = PhotonNetwork.IsMasterClient ? gameManager.Player1 : gameManager.Player2;

        Debug.Log(player.Name);

        UpdateStatusText(); // Инициализируем начальный текст статуса
    }

    public void DecrementShip(int selectedShipLength)
    {
        if (selectedShipLength < 1 || selectedShipLength > 4)
        {
            Debug.LogError($"Неверная длина корабля: {selectedShipLength}. Допустимые длины: 1, 2, 3 или 4.");
            return;
        }

        int index = 4 - selectedShipLength;

        if (index < 0 || index >= shipCount.Length)
        {
            Debug.LogError($"Неверный индекс: {index}. Индекс для корабля длиной {selectedShipLength} выходит за пределы.");
            return;
        }

        if (shipCount[index] == null)
        {
            Debug.LogError($"Значение в shipCount для индекса {index} равно null.");
            return;
        }

        try
        {
            shipCount[index].text = ShipCountTextParser.ParseAndSubtract(shipCount[index].text);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при изменении количества кораблей: {ex.Message}");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Игрок не инициализирован.");
            return;
        }

        if (player.shipCounts == null || !player.shipCounts.ContainsKey(selectedShipLength))
        {
            Debug.LogError($"У игрока отсутствует запись для корабля длиной {selectedShipLength}.");
            return;
        }

        if (--player.shipCounts[selectedShipLength] == 0)
        {
            shipCount[index].color = new Color(0.3843138f, 0.3921569f, 0.4f, 1f);

            if (shipBtn != null && index >= 0 && index < shipBtn.Length)
            {
                shipBtn[index].interactable = false;
            }
            else
            {
                Debug.LogError($"Кнопка для корабля длиной {selectedShipLength} не найдена.");
            }

            FindObjectOfType<ShipPlacementManager>().selectedShipLength--;
        }

        if (--player.ShipsLeft == 0)
        {
            readyBtn.interactable = true;
        }

        UpdateStatusText(); // Обновляем текст статуса
    }

    public void SetEmptyStatusString()
    {
        statusText.text = "";
    }

    public void UpdateStatusText()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        // Проверяем, есть ли оба игрока в комнате
        if (PhotonNetwork.PlayerList.Length < 2)
        {
            localizedWaitingToStartOpponent.StringChanged += value => statusText.text = value;
            localizedWaitingToStartOpponent.RefreshString();
            return;
        }

        // Этап 1: Установка кораблей
        if (player.ShipsLeft > 0)
        {
            localizedPlaceShips.StringChanged += value => statusText.text = value;
            localizedPlaceShips.RefreshString();
            return;
        }

        // Этап 2: Проверяем, локальный игрок нажал Ready
        bool isLocalPlayerReady = PhotonNetwork.IsMasterClient ? gameManager.player1Ready : gameManager.player2Ready;

        if (!isLocalPlayerReady)
        {
            localizedConfirmReady.StringChanged += value => statusText.text = value;
            localizedConfirmReady.RefreshString();
            return;
        }

        // Этап 3: Проверяем, началась ли игра
        if (!gameManager.player1Ready || !gameManager.player2Ready)
        {
            // Показать "Ждём готовности соперника"
            localizedWaitingOpponent.StringChanged += value => statusText.text = value;
            localizedWaitingOpponent.RefreshString();
            return;
        }

        // Этап 4: Игра началась
        if (player.IsTurn)
        {
            localizedYourTurn.StringChanged += value => statusText.text = value;
            localizedYourTurn.RefreshString();
        }
        else
        {
            localizedOpponentTurn.StringChanged += value => statusText.text = value;
            localizedOpponentTurn.RefreshString();
        }
    }

}
}

