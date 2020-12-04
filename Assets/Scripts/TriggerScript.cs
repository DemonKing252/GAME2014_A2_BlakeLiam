using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: TriggerScript.cs
Author: Liam Blake
Created: 2020-11-11
Modified: 2020-11-11
*/
public class TriggerScript : MonoBehaviour
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
        transform.parent.GetComponent<SpikeController>().TrigEnter(collision);
    }
}
