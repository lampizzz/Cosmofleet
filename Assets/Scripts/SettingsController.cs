using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance { get; private set;}
    
    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
