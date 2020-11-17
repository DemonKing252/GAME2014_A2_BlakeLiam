using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingArm : MonoBehaviour
{
    [SerializeField]
    int boneNum;

    bool hooked = false;

    bool playerAttached = false;

    JoyStickController joyStick;
    GameObject player;
    PlayerController playerCont;
    Rigidbody2D rb;

    bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        joyStick = FindObjectOfType<JoyStickController>();
        player = FindObjectOfType<PlayerController>().gameObject;
        playerCont = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(FindObjectOfType<JoyStickController>().localJoystickP.y);


        if (playerAttached)
        {
            //Debug.Log(boneNum.ToString());
            Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.0f, 220.0f), Mathf.Clamp(transform.position.y, 0.0f, 18.9f), -10.0f);

            player.transform.position = transform.position;

            if (joyStick.localJoystickP.x > 0.3f)
            {
                rb.AddForce(Vector2.right * 6.0f);
            }
            else if (joyStick.localJoystickP.x < -0.3f)
            {
                rb.AddForce(Vector2.left * 6.0f);
            }
        }
        if (joyStick.localJoystickP.y < 0.4f && playerAttached)
        {
            playerAttached = false; 
            playerCont.attached = false;

            player.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 2.5f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && joyStick.localJoystickP.y >= 0.4f && !playerCont.attached)
        {
            playerAttached = true;
            playerCont.attached = true;
            collided = true;
        }
    }
}
