using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    [SerializeField]
    GameObject joystick;

    [SerializeField]
    GameObject player;

    public PlayerController playerController;

    public Vector2 localJoystickP;

    [SerializeField]
    public float minJoystickSens;

    [SerializeField]
    LayerMask platforms;

    [SerializeField]
    LayerMask noJump;

    bool held = false;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool touched = false;
        foreach (var touch in Input.touches)
        {
            touched = true;
            var p = Camera.main.ScreenToWorldPoint(touch.position);

            if (GetComponent<CircleCollider2D>().OverlapPoint(p) || held)
            {
                held = true;

                // Local coordinate of the touch position relative to the joystick:
                Vector3 lP = new Vector3(p.x, p.y, joystick.transform.position.z) - transform.position;

                // Prevent the stick from leaving the joystick:
                if (Vector3.Magnitude(lP) >= 1.0f)
                    lP.Normalize(); // Length back to 1

                joystick.transform.position = transform.position + lP;
                localJoystickP = lP;
                //Debug.Log(joystick.transform.localPosition.ToString());
            }


        }
        if (!touched)
        {
            held = false;
            // If the user is not touching/releases their finger, reset the stick postion to be back in the centre:
            joystick.transform.position = transform.position;
            localJoystickP = Vector2.zero;
        }
        // Ray cast fired below the player to detect collisions:
        RaycastHit2D rayCast = Physics2D.CapsuleCast(player.GetComponent<CapsuleCollider2D>().bounds.center, player.GetComponent<CapsuleCollider2D>().bounds.size, player.GetComponent<CapsuleCollider2D>().direction, 0.0f, Vector2.down, 0.1f, platforms);
        RaycastHit2D rayCast1 = Physics2D.CapsuleCast(player.GetComponent<CapsuleCollider2D>().bounds.center, player.GetComponent<CapsuleCollider2D>().bounds.size, player.GetComponent<CapsuleCollider2D>().direction, 0.0f, Vector2.down, 0.1f, noJump);
        
        // Ray cast fired to the right to prevent players from getting stuck on the side of platforms:
        RaycastHit2D rayCastXP = Physics2D.CapsuleCast(player.GetComponent<CapsuleCollider2D>().bounds.center, player.GetComponent<CapsuleCollider2D>().bounds.size, player.GetComponent<CapsuleCollider2D>().direction, 0.0f, Vector2.right, 0.1f, noJump);

        // Ray cast fired to the left to prevent players from getting stuck on the side of platforms:
        RaycastHit2D rayCastXN = Physics2D.CapsuleCast(player.GetComponent<CapsuleCollider2D>().bounds.center, player.GetComponent<CapsuleCollider2D>().bounds.size, player.GetComponent<CapsuleCollider2D>().direction, 0.0f, Vector2.left, 0.1f, noJump);


        if (localJoystickP.x > minJoystickSens && rayCastXP.collider == null && !player.GetComponent<PlayerController>().attached)
        {

            player.GetComponent<SpriteRenderer>().flipX = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(localJoystickP.x * playerController.maxVelX * Time.deltaTime, -5.0f, 5.0f), player.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (localJoystickP.x < -minJoystickSens && rayCastXN.collider == null && !player.GetComponent<PlayerController>().attached)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(localJoystickP.x * playerController.maxVelX * Time.deltaTime, -5.0f, 5.0f), player.GetComponent<Rigidbody2D>().velocity.y);
        }

        Debug.Log(player.GetComponent<Rigidbody2D>().velocity.ToString());
        if (rayCast.collider != null || rayCast1.collider != null && !player.GetComponent<PlayerController>().attached)
        {
            //Debug.DrawRay(player.GetComponent<CapsuleCollider2D>().bounds.center, Vector2.down * 0.1f, Color.green, 2.0f);

            playerController.grounded = true; 
            if (localJoystickP.y > 0.7f)
            {
                //Debug.Log("Jumping");
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, Mathf.Clamp(playerController.jumpVel * Time.deltaTime, 0.0f, playerController.maxJumpVel));
            }

        }
        else
            playerController.grounded = false;

    }
}
