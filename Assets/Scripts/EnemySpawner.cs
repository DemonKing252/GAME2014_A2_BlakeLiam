using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: EnemySpawner.cs
Author: Liam Blake
Created: 2020-12-03
Modified: 2020-12-03
*/
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Info")]
    [SerializeField]
    List<GameObject> spawners;

    [SerializeField]
    float spawnRate;

    [Header("Enemy Prefabs")]
    [SerializeField]
    List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        HandleSpawning();
    }

    public void Spawn()
    {
        foreach (var spawner in spawners)
        {
            // is the spawner within the bounds of the camera?
            // otherwise we are spawning enemies that the player isnt anywhere near, thus wasting memory and overcrowding
            if (spawner.transform.position.x < Camera.main.transform.position.x + 20.0f && // left
                spawner.transform.position.x > Camera.main.transform.position.x - 20.0f && // right
                spawner.transform.position.y < Camera.main.transform.position.y + 30.0f && // top
                spawner.transform.position.y > Camera.main.transform.position.y - 30.0f) // bottom
            {

                Instantiate(enemies[Random.Range(0, enemies.Count)], spawner.transform.position, Quaternion.identity);
            }
        }
    }
    public void HandleSpawning()
    {
        // 12-40 seconds
        InvokeRepeating("Spawn", 1f, spawnRate);
    }
}
