using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set;}

    private void Awake() {
        if (Instance != null && Instance != this) 
        {
            Destroy(Instance);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
