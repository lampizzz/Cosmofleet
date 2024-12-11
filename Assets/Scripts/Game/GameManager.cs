using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{
    public BattlePlayer Player1 { get; set; }
    public BattlePlayer Player2 { get; set; }

    private void Awake()
    {
        // Инициализация игроков
        Player1 = new BattlePlayer("Player1");
        Player2 = new BattlePlayer("Player2");

        // Начинает первый игрок
        Player1.IsTurn = true;
    }
}
