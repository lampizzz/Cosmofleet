using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuConroller : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject placementPanel;
    [SerializeField] GameObject attackingPanel;

    private void Awake()
    {
        DisableGamePanels();
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
}
