using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyStateMachine _context, EnemyStateFactory _factory) :
        base(_context, _factory)
    { }


    public override void EnterState()
    {
        Debug.Log("Patroling!");
        _ctx.StartCoroutine(FollowPath(_ctx.Waypoints));
    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (_ctx.IsPlayerVisible)
        {
            SwitchState(_factory.Chase());
        }

    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 directionToTarget = (lookTarget - _ctx.Enemy.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;


        while (Mathf.Abs(Mathf.DeltaAngle(_ctx.Enemy.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(_ctx.Enemy.eulerAngles.y, targetAngle, _ctx.TurnSpeed * Time.fixedDeltaTime);
            _ctx.Enemy.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }


    IEnumerator FollowPath(Vector3[] waypoints)
    {
        _ctx.Enemy.position = waypoints[_ctx.CurrentIndex];

        Vector3 targetWayPoint = waypoints[_ctx.CurrentIndex++];
        _ctx.Enemy.LookAt(targetWayPoint);

        while (true)
        {
            _ctx.Enemy.position = Vector3.MoveTowards(_ctx.Enemy.position, targetWayPoint, _ctx.Speed * Time.deltaTime);
            if (_ctx.Enemy.position == targetWayPoint)
            {
                _ctx.CurrentIndex = (_ctx.CurrentIndex + 1) % waypoints.Length;
                targetWayPoint = waypoints[_ctx.CurrentIndex];
                yield return new WaitForSeconds(0.5f);
                yield return _ctx.StartCoroutine(TurnToFace(targetWayPoint));

            }
            yield return null;
        }
    }
}
