using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogCollector : MonoBehaviour
{
    public static LogCollector Instance { get; private set; }

    private List<string> logMessages = new List<string>(); // Список для хранения сообщений логов
    private string logFilePath; // Путь к файлу логов

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Устанавливаем путь для логов (например, папка Documents)
        logFilePath = Path.Combine(Application.persistentDataPath, "UnityLogs.txt");

        // Подписываемся на событие логов
        Application.logMessageReceived += HandleLogMessage;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLogMessage;
        SaveLogsToFile(); // Дополнительный вызов на случай, если объект уничтожится до OnApplicationQuit
    }

    private void OnApplicationQuit()
    {
        SaveLogsToFile();
    }

    private void HandleLogMessage(string logString, string stackTrace, LogType type)
    {
        string formattedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{type}] {logString}";
        if (type == LogType.Error || type == LogType.Exception)
            formattedMessage += $"\nStack Trace:\n{stackTrace}";
        
        logMessages.Add(formattedMessage);
    }

    private void SaveLogsToFile()
    {
        try
        {
            File.WriteAllLines(logFilePath, logMessages);
            Debug.Log($"Logs saved to {logFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save logs to file: {e.Message}");
        }
    }
}