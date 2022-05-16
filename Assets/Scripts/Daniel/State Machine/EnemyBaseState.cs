public abstract class EnemyBaseState
{
    protected EnemyStateMachine _ctx;
    protected EnemyStateFactory _factory;
    public EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
    {
        _ctx = currentContext;
        _factory = stateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();


    protected void SwitchState(EnemyBaseState newState)
    {
        ExitState();
        newState.EnterState();
        _ctx.CurrentState = newState;
    }

}
