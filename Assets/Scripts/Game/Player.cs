using UnityEngine;

public class BattlePlayer 
{
    public string Name { get; private set; }
    public Field PlacementField { get; private set; }
    public Field AttackField { get; private set; }
    public bool IsTurn { get; set; }
    public int ShipsLeft { get; private set; }

    private int[] shipCounts; // Индексы: 0 - L1, 1 - L2, 2 - L3, 3 - L4

    public BattlePlayer(string name)
    {
        Name = name;
        PlacementField = new Field();
        AttackField = new Field();
        ShipsLeft = 10; // 1xL4 + 2xL3 + 3xL2 + 4xL1
        shipCounts = new[] { 4, 3, 2, 1 };
        IsTurn = false;
    }

    public bool PlaceShip(int startX, int startY, int length, bool isHorizontal)
    {
        if (shipCounts[4 - length] > 0)
        {
            bool success = PlacementField.PlaceShip(startX, startY, length, isHorizontal);
            if (success)
            {
                shipCounts[4 - length]--;
                return true;
            }
        }
        return false;
    }

    public void RegisterHit()
    {
        ShipsLeft--;
    }

    public int GetShipCount(int length)
    {
        return shipCounts[4 - length];
    }

    public bool HasShipsRemaining()
    {
        return ShipsLeft > 0;
    }
}