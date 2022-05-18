using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWinState : EnemyBaseState
{
    public EnemyWinState(EnemyStateMachine _context, EnemyStateFactory _factory) :
        base(_context, _factory)
    { }

    public override void EnterState()
    {
        GameManager.current.Die = true;
        Time.timeScale = 0;
    }

    public override void ExitState()
    {
        //talvez desnecessario neste contexto
    }

    public override void UpdateState()
    {
        // provavelmente verificar se o jogador quer recomecar o jogo
    }
}

