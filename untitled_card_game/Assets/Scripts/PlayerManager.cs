using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject PlayerDropZone;
    public GameObject EnemyDropZone;

    // List of cards
    public List<GameObject> Cards;

    // Sync variable
    [SyncVar]
    int cardsPlayed = 0;

    [SyncVar]
    public GameObject SelectedCard;

    [SyncVar]
    public GameObject AttackedCard;

    // Called on client start instead of Start
    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");
        PlayerDropZone = GameObject.Find("PlayerDropZone");
        EnemyDropZone = GameObject.Find("EnemyDropZone");
    }

    [Server]

    // Server alone handles list of cards to draw from
    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 5; i++)
        {
            // Instantiate a new card 5 times
            GameObject newCard = Instantiate(Cards[Random.Range(0, Cards.Count)], new Vector2(0, 0), Quaternion.identity);

            // Spawn card, authority given to client
            NetworkServer.Spawn(newCard, connectionToClient);

            // Call the Rpc
            RpcShowCard(newCard, "Dealt");
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
        cardsPlayed++;
        Debug.Log(cardsPlayed);
    }

    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc]
    // Tell the clients to show the cards
    void RpcShowCard(GameObject card, string type)
    {
        // Is card being dealt
        if (type == "Dealt")
        {
            // If client that PlayerManager exists in has authority (card belongs to this player)
            // place in PlayerArea. If enemy card, place in EnemyArea
            if (hasAuthority)
            {
                card.transform.SetParent(PlayerArea.transform, false);
            }
            else
            {
                card.transform.SetParent(EnemyArea.transform, false);
                card.GetComponent<CardFlipper>().Flip(card);
            }
        }
        // Card type Played
        else if (type == "Played")
        {
            if (!hasAuthority)
            {
                card.transform.SetParent(EnemyDropZone.transform, false);
                card.GetComponent<CardFlipper>().Flip(card);
            }
            else
            {
                card.transform.SetParent(PlayerDropZone.transform, false);
            }
        }
    }

    [Command]
    public void CmdDamagePlayer(GameObject player, int damage)
    {
        RpcDamagePlayer(player, damage);
    }

    [ClientRpc]
    void RpcDamagePlayer(GameObject player, int damage)
    {
        int health = player.GetComponent<PlayerDisplay>().health;

        health -= damage;

        player.GetComponent<PlayerDisplay>().health = health;
        player.GetComponent<PlayerDisplay>().healthText.text = health.ToString();

        Debug.Log("Player took " + damage.ToString() + " damage");
    }

    [Command]
    public void CmdDamage(GameObject damagedCard, int damage)
    {
        RpcDamage(damagedCard, damage);
    }

    [ClientRpc]
    void RpcDamage(GameObject damagedCard, int damage)
    {
        int health = damagedCard.GetComponent<CardDisplay>().health;

        health -= damage;

        damagedCard.GetComponent<CardDisplay>().health = health;
        damagedCard.GetComponent<CardDisplay>().healthText.text = health.ToString();

        string damagedName = damagedCard.GetComponent<CardDisplay>().nameText.text;

        Debug.Log(damagedName + " took " + damage.ToString() + " damage");

        if (health <= 0)
        {
            if (hasAuthority)
            {
                CmdDestroy(damagedCard);
            }
        }
    }

    [Command]
    void CmdDestroy(GameObject destroyedCard)
    {
        NetworkServer.Destroy(destroyedCard);

        RpcHideZoom(destroyedCard);

        string destroyedName = destroyedCard.GetComponent<CardDisplay>().nameText.text;

        RpcDestroyMessage(destroyedName);
    }

    [ClientRpc]
    void RpcDestroyMessage(string destroyedName)
    {
        Debug.Log(destroyedName + " was killed");
    }

    [ClientRpc]
    void RpcHideZoom(GameObject destroyedCard)
    {
        destroyedCard.GetComponent<CardZoom>().OnHoverExit();
    }
}
