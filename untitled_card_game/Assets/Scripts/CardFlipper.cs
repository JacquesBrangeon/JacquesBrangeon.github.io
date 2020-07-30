using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardFlipper : NetworkBehaviour
{
    public GameObject CardBack;
    public Sprite CardBackSprite;
    public Sprite TransCardBack;

    //[ClientRpc]
    public void Flip(GameObject card)
    {
        if (card.GetComponent<CardDisplay>().isCardFlipped)
        {
            CardBack.GetComponent<Image>().sprite = TransCardBack;
            card.GetComponent<CardDisplay>().Flipped();
        }
        else
        {
            CardBack.GetComponent<Image>().sprite = CardBackSprite;
            card.GetComponent<CardDisplay>().Flipped();
        }
    }
}
