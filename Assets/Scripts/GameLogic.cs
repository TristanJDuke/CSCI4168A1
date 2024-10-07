using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    
    public GameObject playerPrefab;
    public Transform spawnPoint;
    private int _scene;
    private void Start()
    {
        _scene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelCompletion(_scene);
        }
    }
    
    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    public void LevelCompletion(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }
    
}
