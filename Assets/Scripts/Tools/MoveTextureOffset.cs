using UnityEngine;

public class MoveTextureOffset : MonoBehaviour
{
    [SerializeField] Renderer targetRenderer; // Рендерер объекта (например, звезды)
    [SerializeField] Vector2 offsetSpeed = new Vector2(0.1f, 0); // Скорость изменения офсета (по X и Y)

    private Material material;

    void Start()
    {
        // Получаем материал из рендерера
        if (targetRenderer != null)
        {
            material = targetRenderer.material;
        }
        else
        {
            Debug.LogError("Не установлен targetRenderer!");
        }
    }

    void Update()
    {
        if (material != null)
        {
            // Вычисляем новый офсет текстуры
            Vector2 currentOffset = material.mainTextureOffset;
            currentOffset += offsetSpeed * Time.deltaTime;

            // Применяем офсет
            material.mainTextureOffset = currentOffset;
        }
    }
}
