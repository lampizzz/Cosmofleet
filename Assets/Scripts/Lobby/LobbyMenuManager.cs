using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyMenuManager : MonoBehaviour
{
    [SerializeField] GameObject lobbyPanel;
    [SerializeField] GameObject roomCreationPanel;

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    public void CreateRoom()
    {
        lobbyPanel.SetActive(false);
        roomCreationPanel.SetActive(true);
    }

    public void BackToLobby()
    {
        roomCreationPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
}
