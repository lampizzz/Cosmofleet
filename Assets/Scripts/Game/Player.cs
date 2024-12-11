using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer 
{
    public string Name { get; private set; }
    public Field PlacementField { get; set; }
    public Field AttackField { get; set; }
    public bool IsTurn { get; set; }
    public int ShipsLeft { get; set; }

    public Dictionary<int, int> shipCounts;

    public BattlePlayer(string name)
    {
        Name = name;
        PlacementField = new Field();
        AttackField = new Field();
        ShipsLeft = 10; // 1xL4 + 2xL3 + 3xL2 + 4xL1
        shipCounts = new Dictionary<int, int>()
        {
            [4] = 1,
            [3] = 2,
            [2] = 3,
            [1] = 4
        };
        IsTurn = false;
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