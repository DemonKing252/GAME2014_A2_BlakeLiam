using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


    [SerializeField]
    public AudioSource deathChannel;


    // Start is called before the first frame update
    void Start()
    { 
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
    bool should_play_swing = true;
    float timeSinceDeath = 0.0f;
    // Update is called once per frame
    void Update()
    {

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
                SceneManager.LoadScene(4);
            }
        }
        


        if (FindObjectOfType<JoyStickController>().localJoystickP.y < -FindObjectOfType<JoyStickController>().minJoystickSens)
        {
            if (should_play_swing)
            {
                swing.Play();
                should_play_swing = false;
            }
            GetComponent<Animator>().SetInteger("State", (int)PlayerState.Swing);
        }
        else
            should_play_swing = true;
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.x);
    }
}
