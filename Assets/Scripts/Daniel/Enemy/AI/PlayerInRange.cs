using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    [Range(0, 5)]
    public float radius;

    [Range(0, 180)]
    public float angle;


    public float turnSpeed;
    public float speed;

    float speedPercent;
    float stoppingDistance = 4;




    public GameObject player;

    public LayerMask obstacleMask;
    public LayerMask targetMask;


    public bool canSeePlayer;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        FieldOfViewCheck();

        if (canSeePlayer)
        {
            FollowPlayer();
        }


    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;


            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;

    }


    public void FollowPlayer()
    {

        speedPercent = Mathf.Clamp01(Vector3.Distance(transform.position, player.transform.position) / stoppingDistance);
        if (speedPercent == 0.01f)
        {
            canSeePlayer = false;
        }

        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        transform.Translate(speed * speedPercent * Vector3.forward * Time.deltaTime);



        if (Vector3.Distance(player.transform.position, transform.position) < 2)
            Debug.Log("Game Over");


    }



}
