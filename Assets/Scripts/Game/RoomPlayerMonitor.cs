using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomPlayerMonitor : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomStatusText; // Ссылка на текстовый объект

    // Вызывается, когда игрок входит в комнату
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        DisableWaitText();
    }

    // Вызывается при старте, если уже есть два игрока
    private void Start()
    {
        DisableWaitText();
    }

    private void DisableWaitText()
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (roomStatusText != null)
            {
                roomStatusText.gameObject.SetActive(false);
            }
        }
    }
}