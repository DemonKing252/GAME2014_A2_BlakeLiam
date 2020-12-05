using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: TreasureSpawner.cs
Author: Liam Blake
Created: 2020-12-04
Modified: 2020-12-04
*/

[System.Serializable]
struct Treasure
{
    [SerializeField]
    public List<Transform> spawnPoints;

}


public class TreasureSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject treasureRef;

    [SerializeField]
    List<Treasure> treasure;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var it in treasure)
        {
            GameObject go = Instantiate(treasureRef, it.spawnPoints[Random.Range(0, it.spawnPoints.Count)].position, Quaternion.identity);
            go.SetActive(true);
        }   
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
