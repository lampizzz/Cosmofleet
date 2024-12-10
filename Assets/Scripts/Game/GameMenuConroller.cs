using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuConroller : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;

    public void QuitGame()
    {
        quitPanel.SetActive(true);
    }

    public void NegativeAnswer()
    {
        quitPanel.SetActive(false);
    }
}
