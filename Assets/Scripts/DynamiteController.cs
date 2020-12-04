using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: DynamiteController.cs
Author: Liam Blake
Created: 2020-11-27
Modified: 2020-11-28
*/
public class DynamiteController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().enabled = false;
        
    }
    float timeSinceExplosion = 0.0f;
    bool damageApplied = false;
    float time = 0.0f;
    public bool explode = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time>=2.0f && !explode)
        {
            GetComponent<Animator>().enabled = true;
            
            explode = true;
        }
        if (explode)
        {
            timeSinceExplosion += Time.deltaTime;
            if (!damageApplied)
            {
                transform.localScale = Vector2.one * 2.0f;
                FindObjectOfType<PlayerController>().explosion.Play();
                damageApplied = true;
                if (FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().IsTouching(GetComponent<CircleCollider2D>()))
                {
                    FindObjectOfType<PlayerController>().UpdateHealth(FindObjectOfType<PlayerController>().health - 40.0f);
                    if (FindObjectOfType<PlayerController>().health <= 0.0f)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().alive = false;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("State", (int)PlayerState.Dead);
                    }
                }
                if (timeSinceExplosion >= GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
