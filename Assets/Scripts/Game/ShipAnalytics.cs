using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipAnalytics : MonoBehaviour
{
    [SerializeField] TMP_Text[] shipCount;
    [SerializeField] Button[] shipBtn;
    [SerializeField] Button readyBtn;
    
    public BattlePlayer player;

    private void Start()
    {
        GameManager gameManager = gameObject.GetComponent<GameManager>();
        player = PhotonNetwork.IsMasterClient ? gameManager.Player1 : gameManager.Player2;

        Debug.Log(player.Name);
    }

    public void DecrementShip(int selectedShipLength)
    {
        // Проверяем, что длина корабля корректна (1, 2, 3 или 4)
        if (selectedShipLength < 1 || selectedShipLength > 4)
        {
            Debug.LogError($"Неверная длина корабля: {selectedShipLength}. Допустимые длины: 1, 2, 3 или 4.");
            return;
        }

        // Вычисляем индекс для корабля по его длине (4 - длина)
        int index = 4 - selectedShipLength;

        // Проверяем, что индекс не выходит за пределы массива
        if (index < 0 || index >= shipCount.Length)
        {
            Debug.LogError($"Неверный индекс: {index}. Индекс для корабля длиной {selectedShipLength} выходит за пределы.");
            return;
        }

        // Проверяем, что текст в shipCount[index] можно корректно обработать
        if (shipCount[index] == null)
        {
            Debug.LogError($"Значение в shipCount для индекса {index} равно null.");
            return;
        }

        // Пробуем уменьшить количество кораблей и обновить текст
        try
        {
            shipCount[index].text = Parser.ParseAndSubtract(shipCount[index].text);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при изменении количества кораблей: {ex.Message}");
            return;
        }

        // Уменьшаем количество оставшихся кораблей у игрока
        if (player == null)
        {
            Debug.LogError("Игрок не инициализирован.");
            return;
        }

        // Проверяем, что у игрока есть ещё корабли этого типа
        if (player.shipCounts == null || !player.shipCounts.ContainsKey(selectedShipLength))
        {
            Debug.LogError($"У игрока отсутствует запись для корабля длиной {selectedShipLength}.");
            return;
        }

        if (--player.shipCounts[selectedShipLength] == 0)
        {
            // Делаем кнопку неактивной, если все корабли данного типа расставлены
            if (shipBtn != null && index >= 0 && index < shipBtn.Length)
            {
                shipBtn[index].interactable = false;
            }
            else
            {
                Debug.LogError($"Кнопка для корабля длиной {selectedShipLength} не найдена.");
            }
        }
        else
        {
            // Если корабли этого типа ещё не расставлены, выводим информацию о оставшемся количестве
            Debug.Log($"Корабль длиной {selectedShipLength} ещё доступен. Осталось: {player.shipCounts[selectedShipLength]}.");
        }

        // Выводим информацию о текущем состоянии игрока для отладки
        Debug.Log($"Игрок: {player.Name}, Осталось кораблей: {player.ShipsLeft}, Оставшиеся корабли: {string.Join(", ", player.shipCounts.Values)}");
        
        if (--player.ShipsLeft == 0)
        {
            readyBtn.interactable = true;
        }
    }
}
