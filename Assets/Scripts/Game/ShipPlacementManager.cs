using System;
using GameClasses;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ShipPlacementManager : MonoBehaviourPun
{
    [SerializeField] Button readyButton;
    [SerializeField] ShipAnalytics analytics;
    
    public GridManager placementGrid; // Грид для расстановки
    public int selectedShipLength = 0; // Длина выбранного корабля
    private bool isHorizontal = true; // Ориентация корабля
    private GameObject[] previewCells; // Ячейки для предпросмотра
    
    [SerializeField] GameManager gm;

    private void Start()
    {
        placementGrid.GenerateGrid(GameClasses.CellType.Placement);
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
                if (cell != null &&
                    cell.GetComponent<Cell>().GetState() == GameClasses.CellState.Default &&
                    !placementGrid.IsAreaAroundOccupied(targetX, targetY))
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
                if (cell == null || cell.GetComponent<Cell>().GetState() != GameClasses.CellState.Hovered)
                {
                    ClearPreview();
                    return;
                }
            }

            foreach (var cell in previewCells)
            {
                if (cell != null)
                {
                    cell.GetComponent<Cell>().SetState(GameClasses.CellState.Occupied);
                }
            }
            
            analytics.DecrementShip(selectedShipLength);
            
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

    public void OnReadyButtonClick()
    {
        readyButton.interactable = false;

        // Получаем матрицу состояний текущей расстановки
        CellState[,] matrix = placementGrid.GetCellStateMatrix();

        // Сериализуем её и отправляем через RPC
        CellState[] serializedMatrix = SerializeMatrix(matrix);

        if (PhotonNetwork.IsMasterClient)
        {
            gm.Player1.PlacementField.SetCellStateMatrix(matrix);
        }
        else
        {
            gm.Player2.PlacementField.SetCellStateMatrix(matrix);
        }

        // Проверяем наличие PhotonView на gm перед вызовом RPC
        if (gm != null && gm.photonView != null)
        {
            gm.photonView.RPC("SetPlayerReady", RpcTarget.AllBuffered, PhotonNetwork.IsMasterClient ? 1 : 2, serializedMatrix);
        }
        else
        {
            Debug.LogError("PhotonView отсутствует на GameManager! RPC не может быть вызван.");
        }
    }


    
    private CellState[] SerializeMatrix(CellState[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        CellState[] serialized = new CellState[rows * cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                serialized[i * cols + j] = matrix[i, j];
            }
        }

        return serialized;
    }
}
