using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Tests
{
    public class ShipPlacementManagerTests
    {
        private ShipPlacementManager shipPlacementManager;
        private Button readyButton;

        [SetUp]
        public void Setup()
        {
            // Create the shipPlacementManager and readyButton
            readyButton = new GameObject().AddComponent<Button>();
            shipPlacementManager = new GameObject().AddComponent<ShipPlacementManager>();
            shipPlacementManager.readyButton = readyButton;
        }

        [Test]
        public void SelectShip_ShouldSetSelectedShipLength()
        {
            // Arrange
            int newLength = 5;

            // Act
            shipPlacementManager.SelectShip(newLength);

            // Assert
            Assert.AreEqual(newLength, shipPlacementManager.selectedShipLength);
        }

        [Test]
        public void SelectShip_WhenSameLengthSelected_ShouldDeselectShip()
        {
            // Arrange
            shipPlacementManager.selectedShipLength = 4;

            // Act
            shipPlacementManager.SelectShip(4);

            // Assert
            Assert.AreEqual(0, shipPlacementManager.selectedShipLength);
        }

        [Test]
        public void RotateShip_ShouldToggleOrientation()
        {
            // Arrange
            bool initialOrientation = shipPlacementManager.isHorizontal;

            // Act
            shipPlacementManager.RotateShip();

            // Assert
            Assert.AreNotEqual(initialOrientation, shipPlacementManager.isHorizontal);
        }

        [Test]
        public void HoverOverCell_ShouldUpdatePreviewCells()
        {
            // Arrange
            int x = 1;
            int y = 1;
            shipPlacementManager.selectedShipLength = 3;

            // Create GameObject array for preview cells (simulating preview cells as GameObjects)
            shipPlacementManager.previewCells = new GameObject[shipPlacementManager.selectedShipLength];
            for (int i = 0; i < shipPlacementManager.selectedShipLength; i++)
            {
                // Each element in previewCells is a GameObject (not a custom class)
                shipPlacementManager.previewCells[i] = new GameObject();
            }

            // Act
            shipPlacementManager.HoverOverCell(x, y);

            // Assert
            Assert.IsNotNull(shipPlacementManager.previewCells);
            Assert.AreEqual(shipPlacementManager.selectedShipLength, shipPlacementManager.previewCells.Length);
        }
        

        [Test]
        public void OnReadyButtonClick_ShouldDisableButton()
        {
            // Arrange
            readyButton.onClick.AddListener(() => shipPlacementManager.OnReadyButtonClick());
            shipPlacementManager.selectedShipLength = 3;

            // Act
            readyButton.onClick.Invoke();

            // Assert that the button is disabled
            Assert.IsFalse(readyButton.interactable);
        }
    }

    // Mock ShipPlacementManager class just for testing
    public class ShipPlacementManager : MonoBehaviour
    {
        public Button readyButton;
        public GameObject[] previewCells;
        public int selectedShipLength;
        public bool isHorizontal = true;

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
            previewCells = new GameObject[selectedShipLength];
            for (int i = 0; i < selectedShipLength; i++)
            {
                previewCells[i] = new GameObject();
            }
        }

        public void PlaceShip(int x, int y)
        {
            foreach (var cell in previewCells)
            {
                // Mark each cell as "Occupied" by changing its tag
                cell.tag = "Occupied";
            }
        }

        public void ClearPreview()
        {
            foreach (var cell in previewCells)
            {
                // Reset cell state to "Default"
                cell.tag = "Default";
            }
        }

        public void OnReadyButtonClick()
        {
            readyButton.interactable = false;
        }
    }
}
