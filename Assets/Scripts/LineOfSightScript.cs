using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<EnemyScript>().canseeplayer)
        {
            transform.parent.GetComponent<EnemyScript>().dir = (FindObjectOfType<PlayerController>().transform.position.x > transform.parent.transform.position.x ? 1.0f : -1.0f);
            transform.parent.transform.localScale = new Vector3(transform.parent.GetComponent<EnemyScript>().dir*0.7f, 0.7f, 0.7f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.parent.GetComponent<EnemyScript>().canseeplayer = true;

            transform.parent.transform.localScale = new Vector3(transform.parent.GetComponent<EnemyScript>().dir * 0.7f, 0.7f, 0.7f);
            transform.parent.GetComponent<EnemyScript>().dir = (FindObjectOfType<PlayerController>().transform.position.x > transform.parent.transform.position.x ? 1.0f : -1.0f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.parent.GetComponent<EnemyScript>().canseeplayer = true;
        }
    }
}
