using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    float angle = 0.0f;
    bool rotateNow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateNow)
        {
            if (angle < 180.0f)
                angle += 1.5f;
            
            Quaternion rot = Quaternion.Euler(0.0f, 0.0f, angle);

            transform.rotation = rot;
        }
        
    }

    public void TrigEnter(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            rotateNow = true;
            //Debug.Log(collider.gameObject.name);
        }
    }
}
