using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Attack : NetworkBehaviour
{
    public GameObject card;
    public GameObject playerDropZone;
    public GameObject enemyDropZone;

    public PlayerManager PlayerManager;

    private int damageTo;
    private int damageFrom;

    // Player is attacked on MouseClick()
    public void PlayerClicked()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        if (PlayerManager.SelectedCard != null)
        {
            damageTo = PlayerManager.SelectedCard.GetComponent<CardDisplay>().attack;

            PlayerManager.CmdDamagePlayer(gameObject, damageTo);
        }
    }

    // Card is selected or attacked on PointerDown
    public void CardClicked()
    {
        playerDropZone = GameObject.Find("PlayerDropZone");
        enemyDropZone = GameObject.Find("EnemyDropZone");

        if (card.transform.parent == playerDropZone.transform)
        {
            SelectCard(card);
        }
        else if (card.transform.parent == enemyDropZone.transform)
        {
            AttackCard(card);
        }
    }

    void SelectCard(GameObject cardClicked)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        PlayerManager.SelectedCard = cardClicked;

        Debug.Log(PlayerManager.SelectedCard.GetComponent<CardDisplay>().nameText.text + " has been selected");
    }

    void AttackCard(GameObject cardClicked)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        PlayerManager.AttackedCard = cardClicked;

        damageTo = PlayerManager.SelectedCard.GetComponent<CardDisplay>().attack;
        damageFrom = PlayerManager.AttackedCard.GetComponent<CardDisplay>().attack;

        PlayerManager.CmdDamage(PlayerManager.AttackedCard, damageTo);
        PlayerManager.CmdDamage(PlayerManager.SelectedCard, damageFrom);

        Debug.Log(PlayerManager.AttackedCard.GetComponent<CardDisplay>().nameText.text + " has been attacked");

        PlayerManager.SelectedCard = null;
        PlayerManager.AttackedCard = null;
    }
}
