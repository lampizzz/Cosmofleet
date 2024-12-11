using UnityEngine;
public class ShipPlacementManager : MonoBehaviour
{
    public GridManager placementGrid;
    public ShipAnalytics shipAnalytics;
    public int selectedShipLength = 0;
    private bool isHorizontal = true;
    private GameObject[] previewCells;

    // Генерирует сетку
    private void Start()
    {
        placementGrid.GenerateGrid(GameClasses.CellType.Placement);
    }
    
    // На пробел меняет ориентацию
     private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateShip();
        }
    }

     // Количество отображаемых ячеек
    public void SelectShip(int length)
    {
        selectedShipLength = selectedShipLength == length ? 0 : length;
    }

    public void RotateShip()
    {
        isHorizontal = !isHorizontal;
    }
    
    // Отображение ячеек при наведении
    public void HoverOverCell(int x, int y)
    {
        ClearPreview();

        if (selectedShipLength > 0)
        {
            bool canPlace = true;
            previewCells = new GameObject[selectedShipLength];

            for (int i = 0; i < selectedShipLength; i++)
            {
                int targetX = isHorizontal ? x + i : x;
                int targetY = isHorizontal ? y : y + i;

                var cell = placementGrid.GetCell(targetX, targetY);
                if (cell != null && cell.GetComponent<Cell>().GetState() == GameClasses.CellState.Default && !placementGrid.IsAreaAroundOccupied(targetX, targetY))
                {
                    previewCells[i] = cell;
                    cell.GetComponent<Cell>().SetState(GameClasses.CellState.Hovered);
                }
                else
                {
                    canPlace = false;
                    break;
                }
            }

            if (!canPlace)
            {
                foreach (var cell in previewCells)
                {
                    if (cell != null)
                    {
                        cell.GetComponent<Cell>().SetState(GameClasses.CellState.Invalid);
                    }
                }
            }
        }
    }

    public void PlaceShip(int x, int y)
    {
        if (previewCells != null && selectedShipLength > 0)
        {
            foreach (var cell in previewCells)
            {
                if (cell != null)
                {
                    cell.GetComponent<Cell>().SetState(GameClasses.CellState.Occupied);
                }
            }
            
            shipAnalytics.DecrementShip(selectedShipLength);
            
            selectedShipLength = 0;
            ClearPreview();
        }
    }

    public void ClearPreview()
    {
        if (previewCells != null)
        {
            foreach (var cell in previewCells)
            {
                if (cell != null)
                {
                    var cellScript = cell.GetComponent<Cell>();
                    if (cellScript.GetState() == GameClasses.CellState.Hovered || cellScript.GetState() == GameClasses.CellState.Invalid)
                    {
                        cellScript.SetState(GameClasses.CellState.Default);
                    }
                }
            }
        }
        previewCells = null;
    }
}