using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerMonitor : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomStatusText; // Ссылка на текстовый объект
    [SerializeField] GameMenuConroller menuController;
    [SerializeField] TMP_Text[] shipCountTexts;
    [SerializeField] Button[] shipTypeBtns;

    // Вызывается, когда игрок входит в комнату
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        DisableWaitText();
        ActivatePanels();
        MakeWhiteCountText();
        ActivateShipButtons();
    }

    // Вызывается при старте, если уже есть два игрока
    private void Start()
    {
        DisableWaitText();
        ActivatePanels();
        MakeWhiteCountText();
        ActivateShipButtons();
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

    private void ActivatePanels()
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            menuController.EnableGamePanels();
        }
    }
    
    private void ActivateShipButtons()
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            foreach (var btn in shipTypeBtns)
            {
                btn.interactable = true;
            }
        }
    }
    
    private void MakeWhiteCountText()
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            foreach (var text in shipCountTexts)
            {
                text.color = Color.white;
            }
        }
    }
}