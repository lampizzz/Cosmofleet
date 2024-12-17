using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] public Color startColor = Color.white; // Начальный цвет текста
    [SerializeField] public Color targetColor = Color.red;  // Цвет, до которого текст мигает
    
    private TMP_Text textMeshPro; // Ссылка на компонент TextMeshPro
    [SerializeField] public float blinkSpeed = 1.0f;        // Скорость мигания

    void Start()
    {
        // Получаем компонент TextMeshPro
        textMeshPro = GetComponent<TMP_Text>();
        if (textMeshPro == null)
        {
            Debug.LogError("Компонент TextMeshPro не найден!");
            enabled = false; // Отключаем скрипт, если компонента нет
            return;
        }

        // Устанавливаем начальный цвет текста
        textMeshPro.color = startColor;
    }

    void Update()
    {
        if (textMeshPro is null) return;

        // Интерполируем цвет между startColor и targetColor
        float t = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
        textMeshPro.color = Color.Lerp(startColor, targetColor, t);
    }
}