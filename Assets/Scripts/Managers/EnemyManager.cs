using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject Enemy;
    public float spawnTime=3f;
    public Transform spawnPoint;
    void Start()
    {
        InvokeRepeating("SpawnEnemy",spawnTime,spawnTime);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        Instantiate(Enemy,spawnPoint.position,spawnPoint.rotation);
    }
}
