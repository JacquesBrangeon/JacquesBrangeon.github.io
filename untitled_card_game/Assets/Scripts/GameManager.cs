using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public Text buttonText;

    public int TurnNumber = 0;
    public string GameState = "Start Game";

    private int ReadyClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = GameState;
    }

    public void ChangeGameState(string stateRequest)
    {
        if (stateRequest == "Start Game")
        {
            ReadyClicks = 0;
            GameState = "Start Game";
        }
        else if (stateRequest == "End Turn")
        {
            if (ReadyClicks >= 1)
            {
                GameState = "End Turn";
            }
        }
        else if (stateRequest == "Draw Card")
        {
            GameState = "Draw Card";
        }
        buttonText.text = GameState;
    }

    public void ChangeReadyClicks()
    {
        ReadyClicks++;
    }

    public void EndGame(GameObject player)
    {
        GameObject enemyHolder = GameObject.Find("EnemyHolder");

        if (player.transform.parent == enemyHolder.transform)
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log("You lose.");
        }
    }
}
