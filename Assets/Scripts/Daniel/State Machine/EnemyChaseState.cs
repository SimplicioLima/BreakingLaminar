using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory) :
        base(currentContext, enemyStateFactory)
    { }


    const float minDistanceToGameLost = 2f;


    public override void EnterState()
    {
        Debug.Log("Chasing Player!");
        //_ctx.StartCoroutine(Sensor());
        _ctx.StartCoroutine(Follow());

    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(_ctx.Enemy.position, _ctx.Player.position) < Mathf.Pow(minDistanceToGameLost, 2f))
        {
            Debug.Log("Game Over!");
            _ctx.StopAllCoroutines();
            SwitchState(_factory.GameLost());
        }

    }


    IEnumerator Sensor()
    {
        RaycastHit hit;
        Vector3 sensorStartPosition = _ctx.Enemy.position;
        sensorStartPosition += _ctx.Enemy.forward * _ctx.SensorAdderVector.z;
        sensorStartPosition += _ctx.Enemy.up * _ctx.SensorAdderVector.y;



        //center sensor
        if (Physics.Raycast(sensorStartPosition, _ctx.Enemy.forward, out hit, _ctx.SensorLength))
        {
            if (hit.collider.CompareTag("Small Obstacle"))
            {
                _ctx.AvoidingObstacle = true;
            }
        }

        //center right sensor
        sensorStartPosition += Vector3.Scale(_ctx.Enemy.right, _ctx.SensorAdderVector);
        if (Physics.Raycast(sensorStartPosition, _ctx.Enemy.forward, out hit, _ctx.SensorLength))
        {
            if (hit.collider.CompareTag("Small Obstacle"))
            {
                _ctx.AvoidingObstacle = true;
            }
        }

        //right sensor
        else if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(_ctx.SensorRotateAngle, _ctx.Enemy.up) * _ctx.Enemy.forward, out hit, _ctx.SensorLength))
        {
            if (hit.collider.CompareTag("Small Obstacle"))
            {
                _ctx.AvoidingObstacle = true;
            }
        }

        //left center sensor
        sensorStartPosition -= Vector3.Scale(_ctx.Enemy.right, _ctx.SensorAdderVector) * 2;
        if (Physics.Raycast(sensorStartPosition, _ctx.Enemy.forward, out hit, _ctx.SensorLength))
        {
            if (hit.collider.CompareTag("Small Obstacle"))
            {
                _ctx.AvoidingObstacle = true;
            }
        }

        //left sensor
        else if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(-_ctx.SensorRotateAngle, _ctx.Enemy.up) * _ctx.Enemy.forward, out hit, _ctx.SensorLength))
        {
            if (hit.collider.CompareTag("Small Obstacle"))
            {
                _ctx.AvoidingObstacle = true;
            }
        }


        if (_ctx.AvoidingObstacle)
        {
            _ctx.Enemy.rotation *= Quaternion.Euler(new Vector3(0, _ctx.TurnSpeed * Time.deltaTime, 0));
        }

        yield return null;


    }



    IEnumerator Follow()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_ctx.Player.position - _ctx.Enemy.position);
        _ctx.Enemy.rotation = Quaternion.Lerp(_ctx.Enemy.rotation, lookRotation, _ctx.TurnSpeed * Time.deltaTime);

        while (_ctx.IsPlayerVisible)
        {
            lookRotation = Quaternion.LookRotation(_ctx.Player.position - _ctx.Enemy.position);
            _ctx.Enemy.rotation = Quaternion.Lerp(_ctx.Enemy.rotation, lookRotation, _ctx.TurnSpeed * Time.deltaTime);
            _ctx.Enemy.position = Vector3.MoveTowards(_ctx.Enemy.position, _ctx.Player.position, _ctx.Speed * Time.deltaTime);
        }
        yield return null;

    }

}
