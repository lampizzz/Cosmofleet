using System;

public class Field
{
    private string[,] grid; // Игровая сетка (10x10)
    public int size = 10; // Размер сетки

    public Field()
    {
        grid = new string[size, size];

        // Инициализация поля пустыми клетками
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = "~"; // Пустая клетка
            }
        }
    }

    // Метод для расстановки корабля
    public bool PlaceShip(int startX, int startY, int length, bool isHorizontal)
    {
        // Проверка, чтобы корабль помещался на поле
        if (isHorizontal)
        {
            if (startX + length > size) return false; // Не влезает по горизонтали
            for (int i = 0; i < length; i++)
            {
                if (grid[startX + i, startY] != "~") return false; // Проверка на пересечение
            }
            for (int i = 0; i < length; i++)
            {
                grid[startX + i, startY] = "="; // Размещение корабля
            }
        }
        else
        {
            if (startY + length > size) return false; // Не влезает по вертикали
            for (int i = 0; i < length; i++)
            {
                if (grid[startX, startY + i] != "~") return false; // Проверка на пересечение
            }
            for (int i = 0; i < length; i++)
            {
                grid[startX, startY + i] = "="; // Размещение корабля
            }
        }

        return true;
    }

    // Метод для атаки на поле
    public bool Attack(int x, int y)
    {
        if (grid[x, y] == "=")
        {
            grid[x, y] = "x"; // Попадание
            return true; // Попал
        }
        else if (grid[x, y] == "~")
        {
            grid[x, y] = "o"; // Промах
            return false; // Промах
        }
        return false; // Если клетка уже атакована
    }

    // Проверка на победу (если все клетки с кораблями потоплены)
    public bool CheckVictory()
    {
        foreach (var cell in grid)
        {
            if (cell == "=") return false; // Если есть незатопленный корабль
        }
        return true; // Все корабли потоплены
    }

    // Для отладки (показ состояния поля)
    public void PrintField()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
