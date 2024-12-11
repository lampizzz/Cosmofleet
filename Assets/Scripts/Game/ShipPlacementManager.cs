using UnityEngine;
public class ShipPlacementManager : MonoBehaviour
{
    public GridManager placementGrid;
    public int selectedShipLength = 0;
    private bool isHorizontal = true;
    private GameObject[] previewCells;

    private void Start()
    {
        placementGrid.GenerateGrid(Cell.CellType.Placement);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateShip();
        }
    }

    public void SelectShip(int length)
    {
        selectedShipLength = selectedShipLength == length ? 0 : length;
    }

    public void RotateShip()
    {
        isHorizontal = !isHorizontal;
    }

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
                if (cell != null && cell.GetComponent<Cell>().GetState() == Cell.CellState.Default && !placementGrid.IsAreaAroundOccupied(targetX, targetY))
                {
                    previewCells[i] = cell;
                    cell.GetComponent<Cell>().SetState(Cell.CellState.Hovered);
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
                        cell.GetComponent<Cell>().SetState(Cell.CellState.Invalid);
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
                    cell.GetComponent<Cell>().SetState(Cell.CellState.Occupied);
                }
            }
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
                    if (cellScript.GetState() == Cell.CellState.Hovered || cellScript.GetState() == Cell.CellState.Invalid)
                    {
                        cellScript.SetState(Cell.CellState.Default);
                    }
                }
            }
        }
        previewCells = null;
    }
}