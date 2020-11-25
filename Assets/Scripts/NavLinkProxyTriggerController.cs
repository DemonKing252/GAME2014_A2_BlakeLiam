﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavLinkProxyTriggerController : MonoBehaviour
{
    [SerializeField]
    LayerMask cpuMasks;


    int x = 0;
    [SerializeField]
    Vector3 Impulse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.GetComponent<EnemyScript>().canseeplayer)
        {
            collision.gameObject.GetComponent<EnemyScript>().jumping = true;
            x++;
            Debug.Log("hi " + x.ToString());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Impulse);
        }
    }
}