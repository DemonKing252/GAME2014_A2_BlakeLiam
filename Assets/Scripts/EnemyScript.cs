using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    public Transform edgeCast, wallCast, linkProxyCast;

    [SerializeField]
    LayerMask floor, walls;

    [SerializeField]
    float maxSpeed = 25.0f;

    [SerializeField]
    public float dir = 1.0f;

    public bool jumping = false;
    float timer = 0.0f;

    public bool canseeplayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _EdgeCheck();
        _LinkProxyCheck();
        _Move();
    }

    private void _Move()
    {
        if (!jumping)
            GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(new Vector2(maxSpeed * dir * Time.deltaTime, GetComponent<Rigidbody2D>().velocity.y), 5.0f);
        else
        {
            timer += Time.deltaTime;
        }

        if (!jumping)
        {

            if (Vector3.Magnitude(GetComponent<Rigidbody2D>().velocity) > 0.1f)
            {
                GetComponent<Animator>().SetInteger("State", 1);
            }
            else
                GetComponent<Animator>().SetInteger("State", 0);
        }
        else
        {
            GetComponent<Animator>().SetInteger("State", 2);
        }
    }

    private void _LinkProxyCheck()
    {
        
    }

    private void _EdgeCheck()
    {
        bool ray = Physics2D.Linecast(transform.position, edgeCast.position, floor);
        bool ray2 = Physics2D.Linecast(transform.position, wallCast.position, walls);
        bool ray3 = Physics2D.Linecast(transform.position, transform.position - new Vector3(0.0f, 1.5f, 0.0f), floor);

        if (!ray)
        {
            dir *= -1f;
            transform.localScale = new Vector3(dir*0.7f, 0.7f, 0.7f);
        }
        else if (ray2)
        {
            dir *= -1f;
            transform.localScale = new Vector3(dir * 0.7f, 0.7f, 0.7f);
        }
        if (ray3 && timer >= 0.2f)
        {
            jumping = false;
            timer = 0.0f;
        }

    }
}
