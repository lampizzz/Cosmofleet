namespace GameClasses
{
    public enum CellState
    {
        Default,   // Свободная клетка
        Hovered,   // Предпросмотр корабля
        Occupied,  // Клетка занята кораблем
        Invalid,   // Недопустимое размещение
        Hit,       // Попадание
        Missed     // Промах
    }
}
