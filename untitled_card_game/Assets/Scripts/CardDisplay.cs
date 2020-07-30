using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardDisplay : NetworkBehaviour
{
    public Card card;

    public Text nameText;

    public Image artworkImage;

    public Text manaText;
    public Text attackText;
    public Text healthText;

    public int manaCost;
    public int health;
    public int attack;
    public int maxHealth;

    public bool isCardFlipped = false;

    // Start is called before the first frame update
    public void Start()
    {
        nameText.text = card.name;
        artworkImage.sprite = card.artwork;
        manaText.text = card.manaCost.ToString();
        attackText.text = card.attack.ToString();
        healthText.text = card.health.ToString();

        manaCost = card.manaCost;
        health = card.health;
        attack = card.attack;

        maxHealth = health;
    }

    public void Flipped()
    {
        isCardFlipped = !isCardFlipped;
    }

    public void Heal(int healValue)
    {
        health += healValue;

        // Can't allow health to be greater than the max
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthText.text = health.ToString();
    }
}
