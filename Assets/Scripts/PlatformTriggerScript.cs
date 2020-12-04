using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: PlatformTriggerScript.cs
Author: Liam Blake
Created: 2020-11-11
Modified: 2020-11-12
*/
public class PlatformTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plat")
            transform.parent.GetComponent<MovingPlatformController>().OnHit(collision);
    }
}
