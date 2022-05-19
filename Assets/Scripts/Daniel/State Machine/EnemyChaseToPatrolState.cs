using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseToPatrolState : EnemyBaseState
{

    public EnemyChaseToPatrolState(EnemyStateMachine _context, EnemyStateFactory _factory) :
      base(_context, _factory)
    { }


    public override void EnterState()
    {
        Debug.Log("Restarting Path!");
        _ctx.StartCoroutine(RestartPatrol(_ctx.Waypoints));
    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (_ctx.Enemy.position == _ctx.Waypoints[_ctx.CurrentIndex])
        {
            SwitchState(_factory.Patrol());
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 directionToTarget = (lookTarget - _ctx.Enemy.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;


        while (Mathf.Abs(Mathf.DeltaAngle(_ctx.Enemy.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(_ctx.Enemy.eulerAngles.y, targetAngle, _ctx.TurnSpeed * Time.deltaTime);
            _ctx.Enemy.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    IEnumerator RestartPatrol(Vector3[] waypoints)
    {
        Vector3 targetWayPoint = waypoints[_ctx.CurrentIndex];
        yield return _ctx.StartCoroutine(TurnToFace(targetWayPoint));

        while (true)
        {
            _ctx.Enemy.position = Vector3.MoveTowards(_ctx.Enemy.position, targetWayPoint, _ctx.Speed * Time.deltaTime);
            yield return null;
        }
    }



}
