using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    bool playerInRange;
    float timer;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;


    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange )
        {
            AttackPlayer();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDied");
        }
    }


    void AttackPlayer()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
    
}
