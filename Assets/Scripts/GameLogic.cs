using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject playerPrefab;  // The player's prefab
    public Transform spawnPoint;     // The spawn point where the player will respawn

    private void Start()
    {
        SpawnPlayer();  // Spawn the player at the start of the game
    }
    
    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);  // Spawn a new player
    }
    void onLevelCompletion()
    {
        SceneManager.LoadScene(2);
    }
    
}
