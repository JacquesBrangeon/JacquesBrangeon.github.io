using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public Text healthText;
    public int health;

    public int maxHealth = 20;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();
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
