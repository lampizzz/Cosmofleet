using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GridManager attackGrid;

    private void Start()
    {
        attackGrid.GenerateGrid(GameClasses.CellType.Attack);
    }

    public bool SelectCellForAttack(int x, int y)
    {
        var cell = attackGrid.GetCell(x, y);
        if (cell != null)
        {
            var cellScript = cell.GetComponent<Cell>();

            if (cellScript.GetState() == GameClasses.CellState.Default)
            {
                // Simulate attack logic
                bool hit = SimulateAttack(x, y);
                cellScript.SetState(hit ? GameClasses.CellState.Hit : GameClasses.CellState.Missed);
                return hit;
            }
        }
        return false; // Уже атаковано
    }

    private bool SimulateAttack(int x, int y)
    {
        // Replace with your logic for checking if a ship is at this location.
        return Random.value > 0.5f; // Example: Randomly simulate hit or miss
    }
}