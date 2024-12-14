using GameClasses;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab; // Префаб ячейки (с Image)
    public int gridSize = 10; // Размер сетки
    public Transform gridParent; // Панель с Grid Layout Group

    private GameObject[,] gridCells; // Хранение ссылок на ячейки

    public void GenerateGrid(GameClasses.CellType type)
    {
        Debug.Log("Generating grid...");
        gridCells = new GameObject[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                gridCells[x, y] = cell;

                var cellScript = cell.GetComponent<Cell>();
                cellScript.SetCoordinates(x, y);
                cellScript.Type = type; // Устанавливаем тип клетки
            }
        }
        Debug.Log("Grid generated.");
    }

    public GameObject GetCell(int x, int y)
    {
        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
        {
            return gridCells[x, y];
        }
        return null;
    }

    public void SetStateGridCell(int x, int y, CellState cellState)
    {
        gridCells[x, y].GetComponent<Cell>().SetState(cellState);
    }

    public bool IsAreaAroundOccupied(int x, int y)
    {
        int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

        for (int i = 0; i < 8; i++) // Проверяем 8 соседей
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            var neighbor = GetCell(nx, ny);
            if (neighbor != null && neighbor.GetComponent<Cell>().GetState() == GameClasses.CellState.Occupied)
            {
                return true;
            }
        }

        return false;
    }


    // Метод для получения матрицы состояний клеток
    public CellState[,] GetCellStateMatrix()
    {
        GameClasses.CellState[,] stateMatrix = new GameClasses.CellState[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                var cell = GetCell(x, y);
                if (cell != null)
                {
                    var cellScript = cell.GetComponent<Cell>();

                    stateMatrix[x, y] = cellScript.GetState(); // Сохраняем состояние клетки в транспонированную матрицу
                }
            }
        }

        // PrintField(stateMatrix);

        return stateMatrix;
    }
    
    // private void PrintField(CellState[,] matrix)
    // {
    //     int size = matrix.GetLength(0);
    //
    //     for (int y = 0; y < size; y++)
    //     {
    //         string row = "";
    //         for (int x = 0; x < size; x++)
    //         {
    //             row += $"{(int)matrix[x, y]} "; // Преобразуем CellState в целое число для упрощения вывода
    //         }
    //         Debug.Log(row);
    //     }
    // }
}
