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
        _ctx.StartCoroutine(FollowPlayer(_ctx.Player.position));

    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (!_ctx.IsPlayerVisible)
        {
            SwitchState(_factory.Patrol());
        }
        if (Vector3.Distance(_ctx.Enemy.position, _ctx.Player.position) < Mathf.Pow(minDistanceToGameLost, 2f))
        {
            Debug.Log("Game Over!");
            _ctx.StopAllCoroutines();
            SwitchState(_factory.GameLost());
        }

    }


    IEnumerator CheckVision()
    {
        Vector3 sensorPosition;
        RaycastHit hit;

        sensorPosition = _ctx.Enemy.position + _ctx.Enemy.forward;

        if (Physics.Raycast(sensorPosition, _ctx.Enemy.forward, out hit, _ctx.SensorLength))
        {
            _ctx.AvoidDistance = (hit.normal.x < 0) ? (-1f) : 1f;
        }
        yield return null;

    }


    IEnumerator FollowPlayer(Vector3 lookTarget)
    {
        float angle = 0;
        Vector3 directionToTarget = (lookTarget - _ctx.Enemy.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(_ctx.Enemy.eulerAngles.y, targetAngle)) > 0.05f)
        {
            angle = Mathf.MoveTowardsAngle(_ctx.Enemy.eulerAngles.y, targetAngle, _ctx.TurnSpeed * Time.fixedDeltaTime);
            _ctx.Enemy.eulerAngles = Vector3.up * angle;
            yield return null;
        }


        while (true)
        {
            yield return _ctx.StartCoroutine(CheckVision());
            angle = Mathf.MoveTowardsAngle(_ctx.Enemy.eulerAngles.y, targetAngle, _ctx.AvoidDistance * _ctx.TurnSpeed * Time.deltaTime);
            _ctx.Enemy.eulerAngles = Vector3.up * angle;
            _ctx.Enemy.position = Vector3.MoveTowards(_ctx.Enemy.position, _ctx.Player.position, _ctx.Speed * Time.deltaTime);
        }

    }

}
