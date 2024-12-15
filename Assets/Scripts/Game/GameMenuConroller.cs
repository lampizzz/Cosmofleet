using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuConroller : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject placementPanel;
    [SerializeField] GameObject attackingPanel;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] GameObject loserPanel;
    
    [SerializeField] TMP_Text winnerScoreText;
    [SerializeField] TMP_Text loserScoreText;

    private void Awake()
    {
        DisableGamePanels();
    }
    
    public void WinGame(int score)
    {
        winnerPanel.SetActive(true);
        winnerScoreText.text = score.ToString();
    }
    
    public void LoseGame(int score)
    {
        loserPanel.SetActive(true);
        loserScoreText.text = score.ToString();
    }

    public void QuitGame()
    {
        quitPanel.SetActive(true);
    }

    public void NegativeAnswer()
    {
        quitPanel.SetActive(false);
    }

    public void DisableGamePanels()
    {
        placementPanel.gameObject.SetActive(false);
        attackingPanel.gameObject.SetActive(false);
    }

    public void EnableGamePanels()
    {
        placementPanel.gameObject.SetActive(true);
        attackingPanel.gameObject.SetActive(true);
    }
}
