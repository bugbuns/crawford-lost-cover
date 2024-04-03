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
    public Transform playerCheckpoint;
    public float timeDelay;
    public float spawnOffset;
    public int numEnemies;
    private bool enemySpawned = false;


    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x > playerCheckpoint.position.x &&!enemySpawned)
        {
            StartCoroutine(spawnEnemies());
            enemySpawned = true;
            Debug.Log("Enemy Spawned");
        }
    }

    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(timeDelay);
        //Spawn the enemies with an offset
        //int offset = 0;
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
        }
    }
}
