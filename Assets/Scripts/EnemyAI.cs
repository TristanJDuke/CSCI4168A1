using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //variables for enemies
    public float damageAmount = 10;
    public float movementSpeed = 2f;
    public float detectionRange = 10f;
    public float rotationSpeed = 5f;
    
    //references
    public Transform player;
    
    //direction assist for enemy
    public Vector3 playerChest = new Vector3(0, 1, 0);
    

    void Update()
    {
        //actively look for players because sometimes they respawn
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            //I couldn't find a better way to do this but unity hates my solution
            if (playerObject != null)
            {
                player = playerObject.transform;//find players location
            }
        }
        //when player is alive
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                //simple enemy moves towards player system
                Vector3 playerPositionWithOffset = player.position + playerChest;
                Vector3 direction = (playerPositionWithOffset - transform.position).normalized;

                //actual movement part
                transform.position = Vector3.MoveTowards(transform.position, playerPositionWithOffset, movementSpeed * Time.deltaTime);

                //added so the model rotates and points towards the player
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    //System for hurting the player when they bump into the enemies
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with player!");
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); //player health script referenced here because it's oopy
            }
        }
    }
}

