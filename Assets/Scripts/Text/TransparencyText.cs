using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTransparency : MonoBehaviour
{
    [SerializeField] TMP_Text loadingText; // Ссылка на текст (например, "Loading...")
    [SerializeField] float fadeSpeed = 1.0f; // Скорость изменения прозрачности

    private Color originalColor; // Исходный цвет текста
    private float alphaDirection = 1.0f; // Направление изменения альфа-канала (увеличение/уменьшение)

    void Start()
    {
        if (loadingText == null)
        {
            Debug.LogError("Текст не привязан! Привяжите объект UI Text в инспекторе.");
            return;
        }

        // Сохраняем исходный цвет текста
        originalColor = loadingText.color;
    }

    void Update()
    {
        if (loadingText == null) return;

        // Изменяем альфа-канал текста
        float newAlpha = loadingText.color.a + alphaDirection * fadeSpeed * Time.deltaTime;

        // Меняем направление, если альфа-канал достиг предела
        if (newAlpha >= 1.0f || newAlpha <= 0.0f)
        {
            alphaDirection *= -1.0f; // Инвертируем направление изменения
            newAlpha = Mathf.Clamp(newAlpha, 0.0f, 1.0f); // Убедимся, что альфа не выходит за границы
        }

        // Обновляем цвет текста с новым значением альфа
        loadingText.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
    }
}