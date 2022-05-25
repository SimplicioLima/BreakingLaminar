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
        _ctx.Agent.stoppingDistance = _ctx.BaseStoppingDistance;
        _ctx.StartCoroutine(RestartPathAgent());

    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (_ctx.PathRestarted)
        {
            SwitchState(_factory.Patrol());
        }
    }

    IEnumerator RestartPathAgent()
    {
        Vector3 targetWaypoint = _ctx.Waypoints[--_ctx.CurrentIndex];

        while (true)
        {
            _ctx.Agent.SetDestination(targetWaypoint);
            if (_ctx.Agent.remainingDistance < _ctx.Agent.stoppingDistance)
                _ctx.PathRestarted = true;
            yield return null;
        }
    }



}
