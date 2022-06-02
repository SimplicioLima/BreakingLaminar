using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisabledState : EnemyBaseState
{

    public EnemyDisabledState(EnemyStateMachine _context, EnemyStateFactory _factory) :
       base(_context, _factory)
    { }




    public override void EnterState()
    {
        Debug.Log("Disabled!");
        StickyBomb.OnEnemyDisabled += StopMovement;
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
            SwitchState(_factory.ChaseToPatrol());
        }
    }


    IEnumerator StopMovement()
    {
        _ctx.Agent.isStopped = true;
        yield return new WaitForSeconds(StickyBomb.waitForReEnable);
        _ctx.Agent.isStopped = false;
        _ctx.Disabled = false;
        StickyBomb.OnEnemyDisabled -= StopMovement;
        MultiStickyBomb.OnMultipleDisabled -= StopMovement;



    }




}
