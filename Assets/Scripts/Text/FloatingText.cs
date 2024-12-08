using UnityEngine;
using DG.Tweening;

public class FloatingText : MonoBehaviour
{
    private Vector3 startPos; // Начальная позиция текста

    [SerializeField] private float floatDistance = 10f; // Дистанция "парения" вверх-вниз
    [SerializeField] private float swayDistance = 5f;   // Дистанция покачивания влево-вправо
    [SerializeField] private float duration = 2f;       // Длительность анимации

    void Start()
    {
        // Сохраняем начальную позицию текста
        startPos = transform.position;

        // Начинаем эффекты
        FloatEffect();
        SwayEffect();
    }

    private void FloatEffect()
    {
        // Анимация движения вверх-вниз
        transform.DOMoveY(startPos.y + floatDistance, duration)
            .SetEase(Ease.InOutSine) // Плавная синусоида
            .SetLoops(-1, LoopType.Yoyo); // Бесконечный цикл туда-сюда
    }

    private void SwayEffect()
    {
        // Анимация покачивания влево-вправо
        transform.DOMoveX(startPos.x + swayDistance, duration / 2) // Длительность покачивания короче для баланса
            .SetEase(Ease.InOutSine) // Плавная синусоида
            .SetLoops(-1, LoopType.Yoyo); // Бесконечный цикл туда-сюда
    }
}
