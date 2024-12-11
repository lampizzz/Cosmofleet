using GameClasses;
public class Field
{

    private CellState[,] grid; // Игровая сетка (10x10)
    public int size = 10; // Размер сетки

    public Field()
    {
        grid = new CellState[size, size];

        // Инициализация поля состояниями Default
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = CellState.Default;
            }
        }
    }

    public CellState GetCellState(int x, int y)
    {
        return grid[x, y];
    }

    public void SetCellState(int x, int y, CellState state)
    {
        grid[x, y] = state;
    }
    
    public void SetCellStateMatrix(CellState[,] newStates)
    {
        int rows = newStates.GetLength(0);
        int columns = newStates.GetLength(1);

        // Перебор всех клеток в матрице состояний
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Устанавливаем состояние клетки на поле
                grid[i, j] = newStates[i, j];
            }
        }
    }
    
    public bool CheckVictory()
    {
        foreach (var cell in grid)
        {
            if (cell == CellState.Occupied) return false; // Если есть незатопленный корабль
        }
        return true; // Все корабли потоплены
    }
}
