using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class checkPointCheck : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPosition;
    public bool enemySpawned = false;
    

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 30f&&!enemySpawned)
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
