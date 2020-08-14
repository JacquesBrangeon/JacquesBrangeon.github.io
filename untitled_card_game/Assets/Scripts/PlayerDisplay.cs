using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public Text healthText;
    public int health;
    public Text manaText;
    public int currentMana;
    public int maxMana;

    public int maxHealth = 20;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();

        manaText.text = currentMana.ToString() + " / " + maxMana.ToString();
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

    public void NewTurnManaChange(int manaNow)
    {
        if (maxMana >= 10)
        {
            // Do nothing
        }
        else
        {
            maxMana = manaNow;
            currentMana = maxMana;
            manaText.text = currentMana.ToString() + " / " + maxMana.ToString();
        }
    }

    public void ManaChange(int manaCost)
    {
        currentMana -= manaCost;
        manaText.text = currentMana.ToString() + " / " + maxMana.ToString();
    }
}
