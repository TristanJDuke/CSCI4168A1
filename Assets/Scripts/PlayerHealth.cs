using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float health; //health vars
    public float maxHealth;
    
    //References
    public Image healthBar;
    private PlayerCharacter _playerCharacter;

    private void Start()
    {
        //initialize max health and attach to player
        maxHealth = health;
        _playerCharacter = GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        //health bar image math
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth ,0,1);
        
        //falling out of the map
        if (transform.position.y < -10f)//fell to death
        {
            _playerCharacter.Die();

        }
    }
    //damage function to be oopy again
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            _playerCharacter.Die();
        }
    }
    
}
