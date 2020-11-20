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
                rb.AddForce(Vector2.right * 800.0f * Time.deltaTime);
            }
            else if (joyStick.localJoystickP.x < -0.3f)
            {
                rb.AddForce(Vector2.left * 800.0f * Time.deltaTime);
            }
        }
        if (joyStick.localJoystickP.y < 0.4f && playerAttached)
        {
            playerAttached = false; 
            playerCont.attached = false;

            Vector2 v = GetComponent<Rigidbody2D>().velocity * 450.0f * Time.deltaTime;

            v = new Vector2(Mathf.Clamp(v.x, -6.0f, +6.0f), Mathf.Clamp(v.y, -6.0f, +6.0f));
            player.GetComponent<Rigidbody2D>().velocity = v;

            Debug.Log(player.GetComponent<Rigidbody2D>().velocity.ToString());
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
