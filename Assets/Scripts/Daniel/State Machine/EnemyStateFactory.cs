public class EnemyStateFactory 
{
    EnemyStateMachine context;

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        context = currentContext;
    }

    public EnemyChaseState Chase()
    {
        return new EnemyChaseState(context,this);
    }

    public EnemyPatrolState Patrol()
    {
        return new EnemyPatrolState(context,this);
    }

    public EnemyDisabledState Disabled()
    {
        return new EnemyDisabledState(context, this);
    }

    public EnemyWinState GameLost()
    {
        return new EnemyWinState(context, this);
    }

    public EnemyMultiDisabledState DisabledByMulti()
    {
        return new EnemyMultiDisabledState(context, this);
    }
    public EnemyRestartPatrolState RestartPatrol()
    {
        return new EnemyRestartPatrolState(context, this);
    }

}
