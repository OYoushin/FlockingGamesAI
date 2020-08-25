using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWayepointDistance = 3f;
    public Transform wolfGFX;
    public GameObject sheepdog;
    [Range(0f, 20f)]
    public float avoidanceRadius = 10f;
    [Range(0f, 30f)]
    public float rePathFindingRadius = 15f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Rigidbody2D rbSheepdog;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        rbSheepdog = sheepdog.GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, GetClosestEnemy(target).position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        if (Vector2.Distance(rbSheepdog.position, rb.position) < avoidanceRadius)
        {

            Vector2 direction = (rb.position - rbSheepdog.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
        }
        else if (Vector2.Distance(rbSheepdog.position, rb.position) > rePathFindingRadius)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWayepointDistance)
            {
                currentWaypoint++;
            }
        }
        //else if (Vector2.Distance(rbSheepdog.position, rb.position) < rePathFindingRadius && Vector2.Distance(rbSheepdog.position, rb.position) > avoidanceRadius)
        //{
        //    //// Maybe try anoter strategy
        //    ///
        //    //Vector2 direction = (new Vector2(-(rbSheepdog.position - rb.position).y, (rbSheepdog.position - rb.position).x) - rb.position).normalized;
        //    //Vector2 force = direction * speed * Time.deltaTime;
        //    //rb.AddForce(force);
        //}

            if (rb.velocity.x >= 0.01f)
        {
            wolfGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            wolfGFX.localScale = new Vector3(1f, 1f, 1f);
        }

    }


    Transform GetClosestEnemy(Transform enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector2 directionToTarget = (Vector2)potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    //Transform GetSuitableEnemy(Transform enemies)
    //{
    //    Transform suitableTarget;

    //    return suitableTarget;
    //}

    private void OnTriggerEnter2D(Collider2D collider)
    { }
  

}

