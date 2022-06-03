using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiDisabledState : EnemyBaseState
{


    public EnemyMultiDisabledState(EnemyStateMachine _context, EnemyStateFactory _factory) :
      base(_context, _factory)
    { }
    public override void EnterState()
    {
        MultiStickyBomb.OnMultipleDisabled += StopMovement;
    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();

    }

    public override void UpdateState()
    {
        if (!_ctx.Agent.isStopped)
        {
            SwitchState(_factory.RestartPatrol());
        }
        if (_ctx.IsPlayerVisible)
        {
            SwitchState(_factory.Chase());
        }
    }

    IEnumerator StopMovement()
    {
        _ctx.Agent.isStopped = true;
        yield return new WaitForSeconds(StickyBomb.waitForReEnable);
        _ctx.Agent.isStopped = false;
        MultiStickyBomb.OnMultipleDisabled -= StopMovement;

    }

}
