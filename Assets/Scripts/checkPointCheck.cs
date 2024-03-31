using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class checkPointCheck : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPosition;
    public Transform playerTransform;
    public bool enemySpawned = false;


    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > 30f&&!enemySpawned)
        {
            spawnEnemies();
            enemySpawned = true;
            Debug.Log("Enemy Spawned");
        }
    }

    void spawnEnemies()
    {
        Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
    }
}
