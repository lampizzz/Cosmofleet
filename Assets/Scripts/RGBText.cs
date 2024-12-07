using UnityEngine;
using TMPro;

public class RGBTextMesh : MonoBehaviour
{
    [SerializeField] float colorChangeSpeed = 1f;
    
    private TMP_Text _text;
    
    void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        // Анимация RGB цвета во времени
        float r = Mathf.Sin(Time.time * colorChangeSpeed) * 0.5f + 0.5f;
        float g = Mathf.Sin(Time.time * colorChangeSpeed + Mathf.PI / 2) * 0.5f + 0.5f;
        float b = Mathf.Sin(Time.time * colorChangeSpeed + Mathf.PI) * 0.5f + 0.5f;

        _text.color = new Color(r, g, b);
    }
}
