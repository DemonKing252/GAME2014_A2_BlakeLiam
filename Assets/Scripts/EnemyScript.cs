using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // 20%
    [SerializeField]
    GameObject dynamite;

    [SerializeField]
    ScoreController scoreController;

    [SerializeField]
    public Transform edgeCast, wallCast, linkProxyCast;

    [SerializeField]
    LayerMask floor, walls;

    [SerializeField]
    float maxSpeed = 25.0f;

    [SerializeField]
    public float dir = 1.0f;

    public bool jumping = false;
    float timer = 0.0f;

    public bool canseeplayer = false;

    public float health = 100.0f;
    bool should_die = false;
    float angle = 0.0f;
    bool spawn = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    [System.Obsolete]
    void FixedUpdate()
    {
        if (!should_die)
        {
            _EdgeCheck();
            _LinkProxyCheck();
            _Move();
        }
      
        _CheckPlayer();
    }

    [System.Obsolete]
    private void _CheckPlayer()
    {
        if (FindObjectOfType<PlayerController>().GetComponentInChildren<BoxCollider2D>().IsTouching(GetComponent<Collider2D>()))
        {
            if (FindObjectOfType<PlayerController>().should_play_swing)
                should_die = true;
             
        }
        if (should_die)
        {
            angle += 3.0f * Time.deltaTime;
            transform.rotation = Quaternion.EulerAngles(0.0f, 0.0f, angle);
            //Debug.Log(angle.ToString());

            if (angle >= 90.0f * Mathf.Deg2Rad)
            {
                FindObjectOfType<PlayerController>().deathChannel.Play();
                scoreController.AddKillToScore();

                int rng = Random.Range(0, 100);
                
                if (rng <= 20 && !spawn)
                {
                    spawn = true;
                    Instantiate(dynamite, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void _Move()
    {

        if (!jumping)
            GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(new Vector2(maxSpeed * dir * Time.deltaTime, GetComponent<Rigidbody2D>().velocity.y), 5.0f);
        else
        {
            timer += Time.deltaTime;
        }

        if (!jumping)
        {

            if (Vector3.Magnitude(GetComponent<Rigidbody2D>().velocity) > 0.1f)
            {
                GetComponent<Animator>().SetInteger("State", 1);
            }
            else
                GetComponent<Animator>().SetInteger("State", 0);
        }
        else
        {
            GetComponent<Animator>().SetInteger("State", 2);
        }
    }

    private void _LinkProxyCheck()
    {
        
    }
   

    private void _EdgeCheck()
    {
        bool ray = Physics2D.Linecast(transform.position, edgeCast.position, floor);
        bool ray2 = Physics2D.Linecast(transform.position, wallCast.position, walls);
        bool ray3 = Physics2D.Linecast(transform.position, transform.position - new Vector3(0.0f, 1.5f, 0.0f), floor);

        if (!ray)
        {
            dir *= -1f;
            transform.localScale = new Vector3(dir*0.7f, 0.7f, 0.7f);
        }
        else if (ray2)
        {
            dir *= -1f;
            transform.localScale = new Vector3(dir * 0.7f, 0.7f, 0.7f);
        }
        if (ray3 && timer >= 0.2f)
        {
            jumping = false;
            timer = 0.0f;
        }

    }
}
