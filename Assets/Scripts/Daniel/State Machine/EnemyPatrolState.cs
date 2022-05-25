using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyStateMachine _context, EnemyStateFactory _factory) :
        base(_context, _factory)
    { }

    Vector3 targetWayPoint;


    public override void EnterState()
    {
        Debug.Log("Patroling!");
        _ctx.Agent.stoppingDistance = _ctx.BaseStoppingDistance;
        targetWayPoint = _ctx.Waypoints[_ctx.CurrentIndex];
        _ctx.StartCoroutine(FollowPathAgent());
    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (_ctx.HasArrivedAtPathHolder)
        {
            _ctx.HasArrivedAtPathHolder = false;
            _ctx.CurrentIndex = (_ctx.CurrentIndex + 1) % _ctx.Waypoints.Length;
            targetWayPoint = _ctx.Waypoints[_ctx.CurrentIndex];
            Debug.Log(_ctx.CurrentIndex);
        }

        if (_ctx.IsPlayerVisible)
        {
            SwitchState(_factory.Chase());
        }

    }

    IEnumerator FollowPathAgent()
    {
        while (true)
        {
            _ctx.Agent.SetDestination(targetWayPoint);

            if (_ctx.Agent.remainingDistance < _ctx.Agent.stoppingDistance)
            {
                _ctx.HasArrivedAtPathHolder = true;
                yield return null;
            }
            yield return null;
        }
    }
}
