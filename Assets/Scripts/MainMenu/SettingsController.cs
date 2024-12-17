using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // Ссылка на объект AudioMixer для управления звуковыми настройками
    [SerializeField] AudioMixer audioMixer;

    /// <summary>
    /// Устанавливает полноэкранный режим игры.
    /// </summary>
    /// <param name="isFullscreen">Если true, включается полноэкранный режим, иначе окно.</param>
    public void SetFullscreen(bool isFullscreen)
    {
        // Устанавливает параметр полноэкранного режима (true - полноэкранный, false - окно)
        Screen.fullScreen = isFullscreen;
    }
    
    /// <summary>
    /// Устанавливает уровень громкости звука через AudioMixer.
    /// </summary>
    /// <param name="volume">Значение громкости в диапазоне от -80 (тихо) до 20 (громко).</param>
    public void SetVolume(float volume)
    {
        // Устанавливает громкость с помощью AudioMixer
        // Параметр "volume" должен быть предварительно настроен в AudioMixer
        audioMixer.SetFloat("volume", volume);
    }
}