using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
