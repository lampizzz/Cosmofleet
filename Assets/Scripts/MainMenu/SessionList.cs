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
    // Префаб для отображения сессии
    [SerializeField] private GameObject sessionPrefab; 
    // Поле для ввода строки поиска
    [SerializeField] private TMP_InputField searchInputField; 
    // Родительский объект для списка отображаемых сессий
    [SerializeField] private Transform contentParent; 
    // Локализованная строка для "Victory" (Победа)
    [SerializeField] private LocalizedString localizedVictory; 
    // Локализованная строка для "Defeat" (Поражение)
    [SerializeField] private LocalizedString localizedDefeat;  
    
    // Коллекция статистики сессий
    private GameStatsCollection statsCollection; 
    // Массив отображаемых префабов сессий
    private GameObject[] displayedSessions; 

    // Метод вызывается при старте сцены
    private void Start()
    {
        // Загружаем данные о сессиях
        statsCollection = SaverLoaderXML.LoadGameSessions();

        if (statsCollection == null || statsCollection.Sessions.Count == 0)
        {
            Debug.Log("No Sessions Loaded");
            return;
        }

        // Подписываемся на событие изменения локали
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

        // Обновляем отображение всех сессий
        UpdateSessionDisplay(statsCollection.Sessions);
    }

    // Метод вызывается при уничтожении объекта
    private void OnDestroy()
    {
        // Отписываемся от события, чтобы избежать утечек памяти
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    /// <summary>
    /// Срабатывает при изменении локали (языка)
    /// </summary>
    private void OnLocaleChanged(Locale newLocale)
    {
        // Обновляем отображение всех сессий в соответствии с новой локалью
        UpdateSessionDisplay(statsCollection.Sessions);
    }

    /// <summary>
    /// Срабатывает при изменении текста в поле поиска
    /// Фильтрует список сессий на основе введённой строки поиска с использованием регулярных выражений.
    /// </summary>
    public void OnSearchInputChanged()
    {
        string searchQuery = searchInputField.text.Trim();

        // Получаем локализованные строки для "Victory" и "Defeat"
        string localizedVictoryText = localizedVictory.GetLocalizedString();
        string localizedDefeatText = localizedDefeat.GetLocalizedString();

        // Если строка поиска пуста, показываем все сессии
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            UpdateSessionDisplay(statsCollection.Sessions);
        }
        else
        {
            // Создаем регулярное выражение для поиска по введённому запросу
            var regex = new Regex(searchQuery, RegexOptions.IgnoreCase);

            // Фильтруем сессии с использованием регулярного выражения
            var filteredSessions = statsCollection.Sessions.Where(session =>
                    regex.IsMatch(session.RoomName ?? "") ||  // Поиск по названию комнаты
                    regex.IsMatch(session.VictoryStatus ? localizedVictoryText : localizedDefeatText) || // Поиск по локализованному статусу победы или поражения
                    regex.IsMatch(session.Score.ToString()) ||  // Поиск по очкам
                    regex.IsMatch(session.Time.ToString("g"))) // Поиск по времени
                .ToList();

            // Обновляем отображение сессий с результатами фильтрации
            UpdateSessionDisplay(filteredSessions);
        }
    }

    /// <summary>
    /// Обновляет отображение сессий на экране.
    /// Удаляет старые элементы и отображает новый список сессий.
    /// </summary>
    private void UpdateSessionDisplay(List<GameStats> sessionList)
    {
        // Удаляем старые элементы, если они существуют
        if (displayedSessions != null)
        {
            foreach (var sessionObject in displayedSessions)
            {
                if (sessionObject != null)
                    Destroy(sessionObject);  // Уничтожаем старые объекты
            }
        }

        // Инициализируем массив для новых отображаемых сессий
        displayedSessions = new GameObject[sessionList.Count];

        // Итерируем список сессий с конца
        for (int i = sessionList.Count - 1, displayIndex = 0; i >= 0; i--, displayIndex++)
        {
            var session = sessionList[i];
            if (session == null) continue; // Пропускаем пустые сессии

            // Создаем новый объект сессии из префаба
            GameObject sessionObj = Instantiate(sessionPrefab, contentParent);

            var sessionComponent = sessionObj.GetComponent<Session>();
            if (sessionComponent == null)
            {
                Debug.LogError("Префаб Session не содержит скрипт Session!");
                continue;
            }

            // Заполняем текстовые поля для сессии
            sessionComponent.timeText.text = session.Time.ToString("g");
            sessionComponent.roomNameText.text = session.RoomName ?? "Unknown Room";
            sessionComponent.scoreText.text = session.Score.ToString();

            // Устанавливаем локализованный текст для победы или поражения
            if (session.VictoryStatus)
            {
                localizedVictory.StringChanged += value =>
                {
                    sessionComponent.victoryStatusText.text = $"<color=green>{value}</color>";
                };
                localizedVictory.RefreshString(); // Принудительное обновление локализованной строки
            }
            else
            {
                localizedDefeat.StringChanged += value =>
                {
                    sessionComponent.victoryStatusText.text = $"<color=red>{value}</color>";
                };
                localizedDefeat.RefreshString(); // Принудительное обновление локализованной строки
            }

            // Сохраняем отображаемый объект сессии
            displayedSessions[displayIndex] = sessionObj;
        }
    }

}
