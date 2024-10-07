using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float damageAmount = 10;
    
    public Transform player;
    public float movementSpeed = 2f;
    public float detectionRange = 10f;
    public float rotationSpeed = 5f;
    public Vector3 playerChest = new Vector3(0, 1, 0);

    void Start()
    {
        
    }

    void Update()
    {
        // Dynamically find the player if the player is not referenced
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;  // Get the player's transform
            }
        }

        // If player is found and within detection range, move towards them
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Calculate direction to the player's center (with offset)
                Vector3 playerPositionWithOffset = player.position + playerChest;
                Vector3 direction = (playerPositionWithOffset - transform.position).normalized;

                // Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, playerPositionWithOffset, movementSpeed * Time.deltaTime);

                // Rotate towards the player
                Quaternion targetRotation = Quaternion.LookRotation(direction);  // Get the rotation towards the player
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  // Smoothly rotate towards the player
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player!");
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}

