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
    float minJoystickSens;

    [SerializeField]
    LayerMask platforms;

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
        RaycastHit2D rayCast = Physics2D.CapsuleCast(player.GetComponent<CapsuleCollider2D>().bounds.center, player.GetComponent<CapsuleCollider2D>().bounds.size, player.GetComponent<CapsuleCollider2D>().direction, 0.0f, Vector2.down, 0.1f, platforms);
        RaycastHit2D rayCastXP = Physics2D.CapsuleCast(player.GetComponent<CapsuleCollider2D>().bounds.center, player.GetComponent<CapsuleCollider2D>().bounds.size, player.GetComponent<CapsuleCollider2D>().direction, 0.0f, Vector2.right, 0.1f, platforms);
        RaycastHit2D rayCastXN = Physics2D.CapsuleCast(player.GetComponent<CapsuleCollider2D>().bounds.center, player.GetComponent<CapsuleCollider2D>().bounds.size, player.GetComponent<CapsuleCollider2D>().direction, 0.0f, Vector2.left, 0.1f, platforms);

        if (localJoystickP.x > minJoystickSens && rayCastXP.collider == null)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(localJoystickP.x * playerController.maxVelX * Time.deltaTime, player.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (localJoystickP.x < -minJoystickSens && rayCastXN.collider == null)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(localJoystickP.x * playerController.maxVelX * Time.deltaTime, player.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (rayCast.collider != null)
        {
            playerController.grounded = true; 
            if (localJoystickP.y > 0.5f)
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, Mathf.Clamp(playerController.jumpVel * localJoystickP.y * Time.deltaTime, 0.0f, playerController.maxJumpVel));
            
        }
        else
            playerController.grounded = false;



    }
}
