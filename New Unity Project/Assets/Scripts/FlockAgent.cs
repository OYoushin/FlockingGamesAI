using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock;  } }
    private bool alive = true;
    private bool obstacleHit = false;
    Collider2D agentCollider;

    public GameObject sheepDog;
    public Rigidbody2D rbSheepDog;
    public Rigidbody2D rbSheep;


    public Collider2D AgentCollider { get { return agentCollider; } }


    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        sheepDog = GameObject.Find("SheepDog");
        rbSheepDog = sheepDog.GetComponent<Rigidbody2D>();
        rbSheep = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public bool IsAlive()
    {
        return alive;
    }

    public bool IsObstacleHit()
    {
        return obstacleHit;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{

        //    transform.up = collision.relativeVelocity;
        //    transform.position += (Vector3)collision.relativeVelocity * Time.deltaTime;
        //}
        if (collision.gameObject.CompareTag("Wolf"))
        {
            alive = false;
            Destroy(gameObject);
            TextScript.sheepDead += 1;
        }
        if (collision.gameObject.CompareTag("SafeHouse"))
        {
            alive = false;
            Destroy(gameObject);
            TextScript.sheepSafe += 1;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            obstacleHit = true;
            transform.up = -collision.relativeVelocity;
            transform.position += (Vector3)collision.relativeVelocity * Time.deltaTime;
        }
    }
}
