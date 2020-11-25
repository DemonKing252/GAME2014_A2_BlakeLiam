using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    float angle = 0.0f;
    bool rotateNow = false;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotateNow)
        {
            if (angle < 180.0f)
                angle += 1.5f;
            
            Quaternion rot = Quaternion.Euler(0.0f, 0.0f, angle);

            transform.rotation = rot;

            timer += Time.deltaTime;

        }
        else if (!rotateNow)
        {
            if (angle > 0.0f)
                angle -= 1.5f;

            Quaternion rot = Quaternion.Euler(0.0f, 0.0f, angle);

            transform.rotation = rot;
        }
        if (timer >= 5.0f)
        {
            timer = 0.0f;
            rotateNow = false;

        }
        
    }

    public void TrigEnter(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            rotateNow = true;
        }
    }
}
