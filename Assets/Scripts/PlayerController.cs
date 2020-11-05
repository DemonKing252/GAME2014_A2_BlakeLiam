using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


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
    public PlayerState playerState;

    private Rigidbody2D rigidBody;
    public bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.Idle;
        rigidBody = GetComponent<Rigidbody2D>();
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

        Debug.Log(GetComponent<Rigidbody2D>().velocity.x);
    }
}
