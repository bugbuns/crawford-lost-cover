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
    public bool enemySpawned;
    public Transform[] scriptedWalkpoints;
    public Transform[] patrolPoints;
    public float timeDelay;
    public float spawnOffset;


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
        GameObject enemy=Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
        
        enemy.GetComponent<EnemyAI>().scriptedPoints = scriptedWalkpoints;
        enemy.GetComponent<EnemyAI>().patrolPoints = patrolPoints;
        enemy.GetComponent<EnemyAI>().hasScriptedMovement = true;
        //enemy.GetComponent<ScriptedMovement>().enabled = true;
        //enemy.GetComponent<EnemyAI>().walkPoint = enemyWalkPoint;
        Destroy(this.gameObject);
    }
}
