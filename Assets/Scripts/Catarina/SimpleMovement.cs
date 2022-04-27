using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public static bool move = true;
    int waypointIndex;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if(dist < 1f)
        {
            IncreaseIndex();
        }

        if(move == true)
        {
            Patrol();
        }
        else{
            transform.Translate(Vector3.forward * 0);
        }
        
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }
}
