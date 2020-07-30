using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PopulateZoom : NetworkBehaviour
{
    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;

    public Text manaText;
    public Text attackText;
    public Text healthText;

    public void Fill(Card hoveredCard)
    {
        nameText.text = hoveredCard.name;
        descriptionText.text = hoveredCard.description;
        artworkImage.sprite = hoveredCard.artwork;
        manaText.text = hoveredCard.manaCost.ToString();
        attackText.text = hoveredCard.attack.ToString();
        healthText.text = hoveredCard.health.ToString();
    }
}
