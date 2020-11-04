using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    [SerializeField]
    GameObject joystick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool touched = false;
        foreach (var touch in Input.touches)
        {
            touched = true;
            var p = Camera.main.ScreenToWorldPoint(touch.position);

            // Local coordinate of the touch position relative to the joystick:
            Vector3 lP = new Vector3(p.x, p.y, joystick.transform.position.z) - transform.position;

            // Prevent the stick from leaving the joystick:
            if (Vector3.Magnitude(lP) >= 1.0f)
                lP.Normalize(); // Length back to 1
            
            joystick.transform.position = transform.position + lP;

            Debug.Log(joystick.transform.localPosition.ToString());
        }
        if (!touched)
        {
            // If the user is not touching/releases their finger, reset the stick postion to be back in the centre:
            joystick.transform.position = transform.position;
        }

    }
}
