using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
File: PlayerController.cs
Author: Liam Blake
Created: 2020-11-04
Modified: 2020-11-29
*/
public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Swing,
    Dead
}


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject abilityRef;

    public Ability ability;

    [SerializeField]
    public AudioSource theme, btnClick, swing, jump;

    public bool attached = false;

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
    public bool alive = true;

    public float speed = 1.0f;

    [SerializeField]
    GameObject endTrigger;

    // may be used elsewhere to avoid having multiple audio variables in sub classes of "enemy" for example
    [SerializeField]
    public AudioSource deathChannel, explosion, pickup;

    public int kills = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(100.0f);
        kills = 0;
        Utilities.kills = 0;
        FindObjectOfType<ScoreController>().score = 0.0f;
        Utilities.score = 0;


        ability = new Ability();
        ability.Zero();


        Screen.SetResolution(3040, 1440, false);

        theme.Play();
        UpdateHealth(100.0f);
        playerState = PlayerState.Idle;

        if (Utilities.scenesChanged == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    // Used elsewhere in the future
    public void UpdateHealth(float newHealth)
    {
        health = newHealth;
        healthBar.GetComponent<Image>().rectTransform.sizeDelta = new Vector2((health / 100.0f) * 500.0f, healthBar.GetComponent<Image>().rectTransform.sizeDelta.y);
    }
    public bool should_play_swing = false;
    float timeSinceDeath = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Collider2D>().IsTouching(endTrigger.GetComponent<Collider2D>()))
        {
            Utilities.score = (int)FindObjectOfType<ScoreController>().score;
            Utilities.kills = kills;
            SceneManager.LoadScene(5);
        }

        if (alive)
        {
            if (!attached)
                Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.0f, 220.0f), Mathf.Clamp(transform.position.y, 0.0f, 18.9f), -10.0f);

            if (grounded && !attached)
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
        }
        else
        {
            timeSinceDeath += Time.deltaTime;

            if (timeSinceDeath >= GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length)
            {
                Utilities.score = (int)FindObjectOfType<ScoreController>().score;
                Utilities.kills = kills;
                SceneManager.LoadScene(4);
            }
        }
        


        if (FindObjectOfType<JoyStickController>().localJoystickP.y < -FindObjectOfType<JoyStickController>().minJoystickSens)
        {
            if (!should_play_swing)
            {
                swing.Play();
                should_play_swing = true;
            }
            GetComponent<Animator>().SetInteger("State", (int)PlayerState.Swing);
        }
        else
            should_play_swing = false;
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.x);
    }
    void FixedUpdate()
    {

        if (abilityRef.GetComponent<AbilityController>().healthTimeRemaining > 0.0f)
            if (health <= 100.0f)
                UpdateHealth(health + (30.0f * Time.deltaTime));

        if (abilityRef.GetComponent<AbilityController>().speedTimeRemaining > 0.0f)
        {
            speed = 1.5f;
        }
        else
            speed = 1.0f;
            
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if ((int)health % 20 == 0)
                deathChannel.Play();

            UpdateHealth(health - 30.0f * Utilities.damageFactor[(int)Utilities.diff] * Time.deltaTime);

            if (health <= 0.0f)
            {
                alive = false;
                GetComponent<Animator>().SetInteger("State", (int)PlayerState.Dead);
            }
        }
    }
}
