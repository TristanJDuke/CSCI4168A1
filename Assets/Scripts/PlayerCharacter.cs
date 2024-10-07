using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private GameLogic gameLogic;  // Reference to the PlayerCharacter script

    
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();

    }
    
    private void Update()
    {
        
    }
    public void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
        gameLogic.SpawnPlayer();
    }
}
