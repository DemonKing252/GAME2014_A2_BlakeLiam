using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: SpikeController.cs
Author: Liam Blake
Created: 2020-11-11
Modified: 2020-11-12
*/
public class SpikeController : MonoBehaviour
{
    float angle = 0.0f;
    bool rotateNow = false;
    float timer = 0.0f;

    [SerializeField]
    Collider2D damageCollider;

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

        if (GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Collider2D>().IsTouching(damageCollider))
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().UpdateHealth(GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().health - (30.0f * Utilities.damageFactor[(int)Utilities.diff] * Time.deltaTime));

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health <= 0.0f)
            {
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().alive = false;
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Animator>().SetInteger("State", (int)PlayerState.Dead);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().deathChannel.Play();
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
