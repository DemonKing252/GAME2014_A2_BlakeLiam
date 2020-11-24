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
    float dir = 1.0f;

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

        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(new Vector2(maxSpeed * dir * Time.deltaTime, GetComponent<Rigidbody2D>().velocity.y), 5.0f);

        if (Vector3.Magnitude(GetComponent<Rigidbody2D>().velocity) > 0.1f)
        {
            GetComponent<Animator>().SetInteger("State", 1);
        }
        else
            GetComponent<Animator>().SetInteger("State", 0);
    }

    private void _LinkProxyCheck()
    {
        
    }

    private void _EdgeCheck()
    {
        bool ray = Physics2D.Linecast(transform.position, edgeCast.position, floor);
        bool ray2 = Physics2D.Linecast(transform.position, wallCast.position, walls);

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

    }
}
