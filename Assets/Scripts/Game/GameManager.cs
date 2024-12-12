using GameClasses;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public BattlePlayer Player1 { get; set; }
    public BattlePlayer Player2 { get; set; }
    public bool player1Ready = false;
    public bool player2Ready = false;

    private void Awake()
    {
        Player1 = new BattlePlayer("MasterPlayer");
        Player2 = new BattlePlayer("GuestPlayer");
    }

    [PunRPC]
    public void SetPlayerReady(int playerIndex, CellState[] serializedMatrix)
    {
        CellState[,] matrix = DeserializeMatrix(serializedMatrix, 10, 10);

        if (!PhotonNetwork.IsMasterClient && playerIndex == 1)
        {
            if (Player1 != null && Player1.PlacementField != null)
            {
                Player1.PlacementField.SetCellStateMatrix(matrix);
                player1Ready = true;
                
                Debug.Log($"Player 1 ({Player1.Name}) Placement Field:");
                PrintField(matrix);
            }
            else
            {
                Debug.LogError("Player 1 or PlacementField is null.");
            }
        }
        else if (PhotonNetwork.IsMasterClient && playerIndex == 2)
        {
            if (Player2 != null && Player2.PlacementField != null)
            {
                Player2.PlacementField.SetCellStateMatrix(matrix);
                player2Ready = true;
                
                Debug.Log($"Player 2 ({Player2.Name}) Placement Field:");
                PrintField(matrix);
            }
            else
            {
                Debug.LogError("Player 2 or PlacementField is null.");
            }
        }
        
        CheckBothPlayersReady();
    }
    
    private void CheckBothPlayersReady()
    {
        if (player1Ready && player2Ready)
        {
            Debug.Log("Both players are ready. Starting the game!");
            StartGame();
        }
    }

    private void StartGame()
    {
        Debug.Log("Game has started!");
        Player1.IsTurn = true;
        Player2.IsTurn = false;

        
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

    private CellState[,] DeserializeMatrix(CellState[] serializedMatrix, int rows, int cols)
    {
        CellState[,] matrix = new CellState[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = serializedMatrix[i * cols + j];
            }
        }

        return matrix;
    }
    
    private void PrintField(CellState[,] matrix)
    {
        int size = matrix.GetLength(0);

        for (int y = 0; y < size; y++)
        {
            string row = "";
            for (int x = 0; x < size; x++)
            {
                row += $"{(int)matrix[x, y]} "; // Преобразуем CellState в целое число для упрощения вывода
            }
            Debug.Log(row);
        }
    }
    
}
