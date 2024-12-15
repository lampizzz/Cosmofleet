using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SessionList : MonoBehaviour
{
    [SerializeField] private GameObject sessionPrefab; // Префаб для отображения сессии
    [SerializeField] private TMP_InputField searchInputField; // Поле поиска
    [SerializeField] private Transform contentParent; // Родительский объект для списка
    [SerializeField] private LocalizedString localizedVictory; // Локализованная строка "Victory"
    [SerializeField] private LocalizedString localizedDefeat;  // Локализованная строка "Defeat"
    
    private GameStatsCollection statsCollection; // Коллекция данных о сессиях
    private GameObject[] displayedSessions; // Отображаемые префабы

    private void Start()
    {
        // Используем Loader для получения данных
        statsCollection = SaverLoaderXML.LoadGameSessions();

        if (statsCollection == null || statsCollection.Sessions.Count == 0)
        {
            Debug.Log("No Sessions Loaded");
            return;
        }

        // Подписка на событие изменения локали
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

        // Обновляем отображение всех сессий
        UpdateSessionDisplay(statsCollection.Sessions);
    }

    private void OnDestroy()
    {
        // Отписываемся от события, чтобы избежать утечки памяти
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    /// <summary>
    /// Срабатывает при смене локали
    /// </summary>
    private void OnLocaleChanged(Locale newLocale)
    {
        // Обновляем отображение всех сессий с учётом новой локали
        UpdateSessionDisplay(statsCollection.Sessions);
    }

    /// <summary>
    /// Фильтрация списка сессий на основе строки поиска с использованием регулярных выражений.
    /// </summary>
    public void OnSearchInputChanged()
    {
        string searchQuery = searchInputField.text.Trim();

        // Получаем локализованные строки для "Victory" и "Defeat"
        string localizedVictoryText = localizedVictory.GetLocalizedString();
        string localizedDefeatText = localizedDefeat.GetLocalizedString();

        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            // Показываем все сессии, если строка поиска пуста
            UpdateSessionDisplay(statsCollection.Sessions);
        }
        else
        {
            // Создаем регулярное выражение для поиска
            var regex = new Regex(searchQuery, RegexOptions.IgnoreCase);

            // Фильтрация сессий с использованием регулярного выражения
            var filteredSessions = statsCollection.Sessions.Where(session =>
                    regex.IsMatch(session.RoomName ?? "") ||
                    regex.IsMatch(session.VictoryStatus ? localizedVictoryText : localizedDefeatText) || // Поиск по локализованному статусу
                    regex.IsMatch(session.Score.ToString()) ||
                    regex.IsMatch(session.Time.ToString("g")))
                .ToList();

            UpdateSessionDisplay(filteredSessions);
        }
    }



    /// <summary>
    /// Обновление списка отображаемых сессий.
    /// </summary>
    private void UpdateSessionDisplay(List<GameStats> sessionList)
    {
        // Удаляем старые элементы
        if (displayedSessions != null)
        {
            foreach (var sessionObject in displayedSessions)
            {
                if (sessionObject != null)
                    Destroy(sessionObject);
            }
        }

        displayedSessions = new GameObject[sessionList.Count];

        // Итерируем список с конца
        for (int i = sessionList.Count - 1, displayIndex = 0; i >= 0; i--, displayIndex++)
        {
            var session = sessionList[i];
            if (session == null) continue; // Защита от null

            GameObject sessionObj = Instantiate(sessionPrefab, contentParent);

            var sessionComponent = sessionObj.GetComponent<Session>();
            if (sessionComponent == null)
            {
                Debug.LogError("Префаб Session не содержит скрипт Session!");
                continue;
            }

            sessionComponent.timeText.text = session.Time.ToString("g");
            sessionComponent.roomNameText.text = session.RoomName ?? "Unknown Room";
            sessionComponent.scoreText.text = session.Score.ToString();

            // Установка локализованного текста
            if (session.VictoryStatus)
            {
                localizedVictory.StringChanged += value =>
                {
                    sessionComponent.victoryStatusText.text = $"<color=green>{value}</color>";
                };
                localizedVictory.RefreshString(); // Принудительное обновление при смене локали
            }
            else
            {
                localizedDefeat.StringChanged += value =>
                {
                    sessionComponent.victoryStatusText.text = $"<color=red>{value}</color>";
                };
                localizedDefeat.RefreshString(); // Принудительное обновление при смене локали
            }

            displayedSessions[displayIndex] = sessionObj;
        }
    }

}
