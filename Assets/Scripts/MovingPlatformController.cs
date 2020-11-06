using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Horizontal,
    Vertical
}

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField]
    Direction direction;

    [SerializeField]
    float maxSpeed;

    private float dir = 1.0f;

    [SerializeField]
    GameObject platform, trigger1, trigger2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platform.transform.position += (direction == Direction.Horizontal ? Vector3.right : Vector3.up) * (dir * maxSpeed * Time.deltaTime);

        if (FindObjectOfType<PlayerController>().GetComponent<Collider2D>().IsTouching(platform.GetComponent<Collider2D>()) && FindObjectOfType<PlayerController>().transform.position.y >= platform.transform.position.y)
        {
            FindObjectOfType<PlayerController>().gameObject.transform.position += (direction == Direction.Horizontal ? Vector3.right : Vector3.up) * (dir * maxSpeed * Time.deltaTime);
        }
    }
    public void OnHit(Collider2D hit)
    {
        dir *= -1.0f;
    }
}
