using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    public GameObject Button;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // OnClick is called on a mouse click
    public void OnClick()
    {
        // Find PlayerManager
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        if (GameManager.GameState == "Start Game")
        {
            StartClick();
        }
        else if (GameManager.GameState == "End Turn")
        {
            EndTurnClick();
        }
        else if (GameManager.GameState == "Draw Card")
        {
            DrawClick();
        }
    }

    void StartClick()
    {
        // Run CmdDealCards
        PlayerManager.CmdDealCards(3);
        //PlayerManager.CmdPlayerSpawn();
    }

    void EndTurnClick()
    {
        if (PlayerManager.IsMyTurn)
        {
            PlayerManager.CmdSwitchTurns();
            GameManager.TurnNumber++;
        }
        else return;
    }

    void DrawClick()
    {
        if (PlayerManager.IsMyTurn)
        {
            PlayerManager.CmdDealCards(1);
            GameManager.ChangeGameState("End Turn");
        }
    }
}
