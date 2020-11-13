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

    // Start is called before the first frame update
    void Start()
    {
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
    // Update is called once per frame
    void Update()
    {


        if (!attached)
            Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.0f, 300.0f), Mathf.Clamp(transform.position.y, 0.0f, 10.0f), -10.0f);
        
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
