﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: MovingPlatformController.cs
Author: Liam Blake
Created: 2020-11-11
Modified: 2020-11-12
*/
public enum Direction
{
    Horizontal,
    Vertical
}

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField]
    Direction direction;

    [SerializeField]
    float maxSpeed;

    private float dir = 1.0f;

    [SerializeField]
    GameObject platform, trigger1, trigger2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platform.transform.position += (direction == Direction.Horizontal ? Vector3.right : Vector3.up) * (dir * maxSpeed * Time.deltaTime);

        if (FindObjectOfType<PlayerController>().GetComponent<Collider2D>().IsTouching(platform.GetComponent<Collider2D>()) && FindObjectOfType<PlayerController>().transform.position.y >= platform.transform.position.y)
        {
            FindObjectOfType<PlayerController>().gameObject.transform.position += (direction == Direction.Horizontal ? Vector3.right : Vector3.up) * (dir * maxSpeed * Time.deltaTime);
        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("TreasureChest");
        foreach (var it in gos)
        {
            if (it.GetComponent<Collider2D>().IsTouching(platform.GetComponent<Collider2D>()))
            {
                it.transform.position += (direction == Direction.Horizontal ? Vector3.right : Vector3.up) * (dir * maxSpeed * Time.deltaTime);
            }
        }
    }
    public void OnHit(Collider2D hit)
    {
        // Reverse direction:
        dir *= -1.0f;
    }
}
