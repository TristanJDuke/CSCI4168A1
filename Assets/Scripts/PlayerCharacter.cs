using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //reference
    private GameLogic _gameLogic;
    void Start()
    {
        _gameLogic = FindObjectOfType<GameLogic>();

    }
    
    private void Update()
    {
        
    }
    public void Die()
    {
        //Debug.Log("Player died!");
        //hes dead so kill him
        Destroy(gameObject);
        //hes got infinite lives so bring him back
        _gameLogic.SpawnPlayer();
    }
}
