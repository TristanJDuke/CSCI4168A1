using UnityEngine.SceneManagement;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    //references
    public GameObject playerPrefab;
    public Transform spawnPoint;
    private int _scene;
    private void Start()
    {
        //using this so I can actually traverse the levels without a menu
        _scene = SceneManager.GetActiveScene().buildIndex;
    }

    //level complete system by touching the end zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelCompletion(_scene);
        }
    }
    
    public void SpawnPlayer()
    {
        //function used by PC to respawn if dead typically.
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    public void LevelCompletion(int sceneIndex)
    {
        //the actual level progression function
        SceneManager.LoadScene(sceneIndex + 1);
    }
    
}
