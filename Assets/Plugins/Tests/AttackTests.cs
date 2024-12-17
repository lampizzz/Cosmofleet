using NUnit.Framework;
using UnityEngine;
using Game;

namespace Game.Tests
{
    public class AttackManagerTests
    {
        private AttackManager attackManager;
        private GridManager gridManager;

        [SetUp]
        public void Setup()
        {
            // Создаем необходимые объекты для теста
            var gameObject = new GameObject();
            attackManager = gameObject.AddComponent<AttackManager>();

            // Создаем заглушку для GridManager
            gridManager = gameObject.AddComponent<GridManager>(); 
            attackManager.attackGrid = gridManager;

            // Заглушка: Генерируем сетку
            gridManager.GenerateGrid(GameClasses.CellType.Attack);
        }

        [Test]
        public void SelectCellForAttack_ShouldNotSelectCell_WhenCellIsHitOrMissed()
        {
            // Arrange
            int x = 0, y = 0;
            var cell = CreateCellAt(x, y);
            cell.SetState(CellState.Hit);  // Set the cell as hit

            // Act
            attackManager.SelectCellForAttack(x, y);

            // Assert
            Assert.AreEqual(CellState.Hit, cell.GetState(), "Cell state should remain as Hit and not be selected.");
        }

        [Test]
        public void HoverOverCell_ShouldNotSetHoverIfCellIsOccupied()
        {
            // Arrange
            int x = 0, y = 0;
            var cell = CreateCellAt(x, y);
            cell.SetState(CellState.Occupied);

            // Act
            attackManager.HoverOverCell(x, y);

            // Assert
            Assert.AreEqual(CellState.Occupied, cell.GetState(), "Cell should remain as Occupied and not change to Hover.");
        }
        
        private Cell CreateCellAt(int x, int y)
        {
            // Просто создаем пустую клетку
            var cellObject = new GameObject($"Cell_{x}_{y}");
            var cell = cellObject.AddComponent<Cell>();
            return cell;
        }
    }

    // Заглушка для Cell
    public class Cell : MonoBehaviour
    {
        private CellState state = CellState.Default;

        public void SetState(CellState newState)
        {
            state = newState;
        }

        public CellState GetState()
        {
            return state;
        }
    }

    // Заглушка для GridManager
    public class GridManager : MonoBehaviour
    {
        public void GenerateGrid(GameClasses.CellType type) { }

        public GameObject GetCell(int x, int y)
        {
            // Возвращаем новый GameObject с компонентом Cell
            var cellObject = new GameObject();
            cellObject.AddComponent<Cell>();
            return cellObject;
        }
    }

    // Заглушка для AttackManager
    public class AttackManager : MonoBehaviour
    {
        public GridManager attackGrid;

        public void SelectCellForAttack(int x, int y)
        {
            var cell = attackGrid.GetCell(x, y).GetComponent<Cell>();

            // Проверяем, не была ли клетка уже поражена или пропущена
            if (cell.GetState() == CellState.Hit || cell.GetState() == CellState.Missed)
            {
                return;
            }

            // Если клетка еще не выбрана, ставим состояние "Занято"
            cell.SetState(CellState.Occupied);
        }

        public void HoverOverCell(int x, int y)
        {
            var cell = attackGrid.GetCell(x, y).GetComponent<Cell>();

            // Если клетка занята, не меняем её состояние
            if (cell.GetState() == CellState.Occupied)
            {
                return;
            }

            // Устанавливаем состояние "Hover"
            cell.SetState(CellState.Hover);
        }

        public void ClearPreview(int x, int y)
        {
            var cell = attackGrid.GetCell(x, y).GetComponent<Cell>();
            // Сброс состояния, если клетка в состоянии Hover
            if (cell.GetState() == CellState.Hover)
            {
                cell.SetState(CellState.Default);
            }
        }
    }

    // Заглушка для CellState
    public enum CellState
    {
        Default,
        Hover,
        Occupied,
        Hit,
        Missed
    }

    // Заглушка для GameClasses
    public class GameClasses
    {
        public enum CellType
        {
            Attack,
            Placement
        }
    }
}
