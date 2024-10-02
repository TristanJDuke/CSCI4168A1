using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private HealthBar healthBar;
    public static PlayerCharacter player { get; private set; }
    public float currentHealth { get; private set; }
    public Slider healthSlider;
    void Start()
    {
        player = this;
        healthBar = PlayerCharacter.player.healthSlider.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) {
            Destroy(this.gameObject);
        }

    }
}
