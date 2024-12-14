using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameClasses;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color availableColor;
    [SerializeField] Color occupiedColor;
    [SerializeField] Color invalidColor;
    [SerializeField] Color hitColor;
    [SerializeField] Color missedColor;
    [SerializeField] Color hoverColor;

    public Vector2Int Coordinates { get; private set; }
    public CellType Type { get; set; } // Тип клетки

    private Image cellImage;
    private CellState currentState;

    private ShipPlacementManager shipPlacementManager;
    private AttackManager attackManager;

    private void Awake()
    {
        cellImage = GetComponent<Image>();
        if (cellImage == null)
        {
            Debug.LogError($"No Image component found on {gameObject.name}");
        }
        SetState(CellState.Default);
    }

    private void Start()
    {
        shipPlacementManager = FindObjectOfType<ShipPlacementManager>();
        attackManager = FindObjectOfType<AttackManager>();
    }

    public void SetCoordinates(int x, int y)
    {
        Coordinates = new Vector2Int(x, y);
    }

    public void SetState(CellState state)
    {
        currentState = state;
        UpdateColor(); // Обновление цвета клетки
    }


    public CellState GetState()
    {
        return currentState;
    }

    private void UpdateColor()
    {
        switch (currentState)
        {
            case CellState.Default:
                cellImage.color = defaultColor;
                break;
            case CellState.Available:
                cellImage.color = availableColor;
                break;
            case CellState.Occupied:
                cellImage.color = occupiedColor;
                break;
            case CellState.Invalid:
                cellImage.color = invalidColor;
                break;
            case CellState.Hit:
                cellImage.color = hitColor;
                break;
            case CellState.Missed:
                cellImage.color = missedColor;
                break;
            case CellState.Hover:
                cellImage.color = hoverColor;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Type == CellType.Placement)
        {
            shipPlacementManager?.HoverOverCell(Coordinates.x, Coordinates.y);
        }
        else if (Type == CellType.Attack)
        {
            attackManager?.HoverOverCell(Coordinates.x, Coordinates.y);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Type == CellType.Placement)
        {
            shipPlacementManager?.ClearPreview();
        }
        else if (Type == CellType.Attack)
        {
            attackManager?.ClearPreview(Coordinates.x, Coordinates.y);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Type == CellType.Placement)
        {
            shipPlacementManager?.PlaceShip(Coordinates.x, Coordinates.y);
        }
        else if (Type == CellType.Attack)
        {
            if (currentState == CellState.Occupied || 
                currentState == CellState.Hit || 
                currentState == CellState.Missed)
            {
                Debug.Log("Cell cannot be selected.");
                return;
            }


            attackManager?.SelectCellForAttack(Coordinates.x, Coordinates.y); // Вызываем SelectCellForAttack
        }
        
    }

}
