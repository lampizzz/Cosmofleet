using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipAnalytics : MonoBehaviour
{
    [SerializeField] TMP_Text[] shipCount;
    [SerializeField] Button[] shipBtn;
    
    public BattlePlayer player;

    private void Start()
    {
        player = GameObject.Find("GameManager").GetComponent<GameManager>().Player1;
        Debug.Log(player.Name);
    }

    public void DecrementShip(int selectedShipLength)
    {
        int index = 4 - selectedShipLength;
        
        Debug.Log(shipCount[index].text);
        shipCount[index].text = Parser.ParseAndSubtract(shipCount[index].text);
        Debug.Log(shipCount[index].text);

        player.ShipsLeft--;
        
        if (--player.shipCounts[selectedShipLength] == 0)
        {
            shipBtn[index].interactable = false;
        }

        ;
    }
}
