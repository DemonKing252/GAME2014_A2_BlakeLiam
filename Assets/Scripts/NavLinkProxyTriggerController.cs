using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: NavLinkProxyTriggerController.cs
Author: Liam Blake
Created: 2020-11-28
Modified: 2020-11-30
*/
public class NavLinkProxyTriggerController : MonoBehaviour
{
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
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Impulse);
        }
    }
}
