using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour
{
    public Text healthText;

    public float maxHealth;

    public static WallController Instance;

    private float currentHealth;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    public void OnHit(float hit)
    {
        currentHealth -= hit;

        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            GameLogic._instance.OnGameOver();
        }
    }
    
    // restart Healt
    public void RestartHealth()
    {
        currentHealth = maxHealth;
    }
}