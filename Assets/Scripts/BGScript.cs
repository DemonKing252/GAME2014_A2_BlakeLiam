using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: BGScript.cs
Author: Liam Blake
Created: 2020-11-10
Modified: 2020-11-10
*/
public class BGScript : MonoBehaviour
{
    [SerializeField]
    float resetX;

    [SerializeField]
    float width;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x - transform.position.x >= resetX)
        {
            transform.position += Vector3.right * (width * 3.0f); // x 3 to bring it back to the front, basically using object pooling
        }
        else if (Camera.main.transform.position.x - transform.position.x <= -resetX)
        {
            transform.position -= Vector3.right * (width * 3.0f); // x 3 to bring it back to the back, basically using object pooling
        }
    }
}
