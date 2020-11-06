﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Dead,
    Swing
}


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float jumpVel;

    [SerializeField]
    public float maxVelX;

    [SerializeField]
    public float maxJumpVel;

    [SerializeField]
    GameObject healthBar; 

    [SerializeField]
    public PlayerState playerState;

    public bool grounded = true;

    public float health;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(100.0f);
        playerState = PlayerState.Idle;
    }
    // Used elsewhere in the future
    public void UpdateHealth(float newHealth)
    {
        health = newHealth;
        healthBar.GetComponent<Image>().rectTransform.sizeDelta = new Vector2((health / 100.0f) * 500.0f, healthBar.GetComponent<Image>().rectTransform.sizeDelta.y);
    }
    // Update is called once per frame
    void Update()
    {

        Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.0f, 300.0f), Camera.main.transform.position.y, -10.0f);
        if (grounded)
        {
            if (GetComponent<Rigidbody2D>().velocity.x < 0.1f && GetComponent<Rigidbody2D>().velocity.x > -0.1f)
            {
                GetComponent<Animator>().SetInteger("State", (int)PlayerState.Idle);
            }
            else
            {
                GetComponent<Animator>().SetInteger("State", (int)PlayerState.Run);
            }
            
        }
        else
        {
            GetComponent<Animator>().SetInteger("State", (int)PlayerState.Jump);
        }

        //Debug.Log(GetComponent<Rigidbody2D>().velocity.x);
    }
}