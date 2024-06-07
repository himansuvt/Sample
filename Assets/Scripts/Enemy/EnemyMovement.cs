using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent agent;
    void Awake()
    {   
        player=GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {        
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth>0 )
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.enabled = false;
        }
            
        
    }
}
