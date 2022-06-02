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
        _ctx.Agent.stoppingDistance = _ctx.BaseStoppingDistance;
        _ctx.StartCoroutine(FollowPathAgent());
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
        if (_ctx.Disabled)
        {
            SwitchState(_factory.Disabled());
        }
    }

    IEnumerator FollowPathAgent()
    {
        Vector3 targetWayPoint = _ctx.Waypoints[_ctx.CurrentIndex];
        while (true)
        {
            _ctx.Agent.SetDestination(targetWayPoint);

            if (_ctx.Agent.remainingDistance <= _ctx.Agent.stoppingDistance)
            {
                Debug.Log("Old index: " + _ctx.CurrentIndex);
                _ctx.CurrentIndex = (_ctx.CurrentIndex + 1) % _ctx.Waypoints.Length;
                Debug.Log("New index: " + _ctx.CurrentIndex);
                targetWayPoint = _ctx.Waypoints[_ctx.CurrentIndex];
            }
            yield return null;
        }
    }
}
