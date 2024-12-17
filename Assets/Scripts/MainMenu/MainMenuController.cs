using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Панели для отображения различных меню
    [SerializeField] GameObject mainMenuPanel;    // Панель основного меню
    [SerializeField] GameObject settingsPanel;    // Панель настроек
    [SerializeField] GameObject historyPanel;     // Панель истории игр

    /// <summary>
    /// Метод для начала новой игры.
    /// Загружает следующую сцену в списке сборки.
    /// </summary>
    public void StartGame()
    {
        // Загружает сцену по индексу, следующему за текущей (например, переход от главного меню к игровому процессу)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Метод для перехода в меню настроек.
    /// Скрывает основное меню и показывает меню настроек.
    /// </summary>
    public void SettingsMenu()
    {
        // Скрыть панель основного меню
        mainMenuPanel.SetActive(false);
        // Показать панель настроек
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Метод для отмены изменения настроек.
    /// Возвращает пользователя в основное меню.
    /// </summary>
    public void CancelSettings()
    {
        // Скрыть панель настроек
        settingsPanel.SetActive(false);
        // Показать панель основного меню
        mainMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Метод для перехода в меню истории.
    /// Скрывает основное меню и отображает панель с историей игр.
    /// </summary>
    public void HistoryMenu()
    {
        // Скрыть панель основного меню
        mainMenuPanel.SetActive(false);
        // Показать панель истории игр
        historyPanel.SetActive(true);
    }

    /// <summary>
    /// Метод для выхода из меню истории и возврата в основное меню.
    /// </summary>
    public void BackFromHistory()
    {
        // Скрыть панель истории игр
        historyPanel.SetActive(false);
        // Показать панель основного меню
        mainMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Метод для выхода из игры.
    /// В Unity не будет работать в редакторе, но вызовет выход из приложения в сборке.
    /// </summary>
    public void ExitGame()
    {
        // Вывод сообщения в консоль
        Debug.Log("Exit");
        // Завершаем приложение
        Application.Quit();
    }
}
