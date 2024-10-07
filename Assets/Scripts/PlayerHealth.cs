using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float health; //health vars
    public float maxHealth;
    public Image healthBar;
    private PlayerCharacter playerCharacter;  // Reference to the PlayerCharacter script

    private void Start()
    {
        maxHealth = health;
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth ,0,1);
        
        if (transform.position.y < -10f)//fell to death
        {
            playerCharacter.Die();

        }
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            playerCharacter.Die();
        }
    }
    
}
