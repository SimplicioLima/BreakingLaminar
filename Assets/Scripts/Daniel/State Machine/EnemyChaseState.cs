using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory) :
        base(currentContext, enemyStateFactory)
    { }


    float minDistanceToGameLost = Mathf.Sqrt(3f);


    public override void EnterState()
    {
        Debug.Log("Chasing Player!");
        _ctx.Agent.stoppingDistance = 3f;
        _ctx.StartCoroutine(FollowPlayerAgent());

    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (_ctx.HasArrivedAtPlayer)
        {
            SwitchState(_factory.GameLost());
        }
        if (!_ctx.IsPlayerVisible)
        {
            SwitchState(_factory.ChaseToPatrol());
        }
        if (_ctx.Disabled)
        {
            SwitchState(_factory.Disabled());
        }

    }

    IEnumerator FollowPlayerAgent()
    {
        while (true)
        {
            _ctx.Agent.SetDestination(_ctx.Player.position);
            if (_ctx.Agent.remainingDistance < _ctx.Agent.stoppingDistance)
                _ctx.HasArrivedAtPlayer = true;
            yield return null;
        }
    }

}
