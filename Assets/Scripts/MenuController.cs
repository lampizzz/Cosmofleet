using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject serversPanel;
    [SerializeField] GameObject settingsPanel;

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        serversPanel.SetActive(true);
    }

    public void SettingsMenu()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CancelSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void CancelServers()
    {
        serversPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
