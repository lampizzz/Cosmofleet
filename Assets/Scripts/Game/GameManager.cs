using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{
    public BattlePlayer Player1 { get; private set; }
    public BattlePlayer Player2 { get; private set; }

    private void Start()
    {
        // Инициализация игроков
        Player1 = new BattlePlayer("Player1");
        Player2 = new BattlePlayer("Player2");

        // Начинает первый игрок
        Player1.IsTurn = true;
    }

    public bool PlaceShip(int playerNumber, int startX, int startY, int length, bool isHorizontal)
    {
        BattlePlayer targetPlayer = playerNumber == 1 ? Player1 : Player2;
        bool result = targetPlayer.PlaceShip(startX, startY, length, isHorizontal);

        if (result)
        {
            photonView.RPC("SyncFieldSetup", RpcTarget.Others, playerNumber, startX, startY, length, isHorizontal);
        }

        return result;
    }

    public bool Attack(int playerNumber, int targetX, int targetY)
    {
        BattlePlayer attacker = playerNumber == 1 ? Player1 : Player2;
        BattlePlayer defender = playerNumber == 1 ? Player2 : Player1;

        if (attacker.IsTurn)
        {
            bool hit = defender.PlacementField.Attack(targetX, targetY);

            // Если попадание, обновляем оставшиеся корабли защитника
            if (hit)
            {
                defender.RegisterHit();
            }

            photonView.RPC("SyncAttackResult", RpcTarget.Others, playerNumber, targetX, targetY, hit);

            // Проверка на победу
            if (!defender.HasShipsRemaining())
            {
                photonView.RPC("EndGame", RpcTarget.All, playerNumber);
            }

            // Переключение хода
            Player1.IsTurn = !Player1.IsTurn;
            Player2.IsTurn = !Player2.IsTurn;

            return hit;
        }

        return false; // Ход не разрешен
    }

    [PunRPC]
    public void SyncFieldSetup(int playerNumber, int startX, int startY, int length, bool isHorizontal)
    {
        BattlePlayer targetPlayer = playerNumber == 1 ? Player1 : Player2;
        targetPlayer.PlaceShip(startX, startY, length, isHorizontal);
    }

    [PunRPC]
    public void SyncAttackResult(int playerNumber, int targetX, int targetY, bool hit)
    {
        BattlePlayer defender = playerNumber == 1 ? Player2 : Player1;
        defender.PlacementField.Attack(targetX, targetY);

        if (hit)
        {
            defender.RegisterHit();
        }
    }

    [PunRPC]
    public void EndGame(int winnerPlayerNumber)
    {
        Debug.Log($"Player {winnerPlayerNumber} wins!");
    }
}
