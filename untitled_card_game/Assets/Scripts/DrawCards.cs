using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    public PlayerManager PlayerManager;

    // OnClick is called on a mouse click
    public void OnClick()
    {
        // Find PlayerManager
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        // Run CmdDealCards
        PlayerManager.CmdDealCards();
    }
}
