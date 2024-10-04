using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private GameLogic spawner;  // Reference to the PlayerSpawner

    
    void Start()
    {
        currentHealth = maxHealth;
        spawner = FindObjectOfType<GameLogic>();
    }
    
    private void Update()
    {
        if (transform.position.y < -10f)//fell to death
        {
            Die();
        }
    }
    public void SpawnPlayer()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage) {
        currentHealth -= damage;//take damage
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int damage)
    {
        currentHealth += damage;//heal damage
        if (currentHealth > 100)
        {
            currentHealth = 100; //no overheal
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        spawner.SpawnPlayer();  // Tell the spawner to respawn a new player
    }
    
    
}
