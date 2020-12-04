using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: TilemapSpikeController.cs
Author: Liam Blake
Created: 2020-11-10
Modified: 2020-11-10
*/
public class TilemapSpikeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>()))
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().UpdateHealth(GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().health - (30.0f * Utilities.damageFactor[(int)Utilities.diff] * Time.deltaTime));
            
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health <= 0.0f)
            {
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().alive = false;
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Animator>().SetInteger("State", (int)PlayerState.Dead);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().deathChannel.Play();
        }
    }
}
