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

    public EnemyObjectSoundState OFSound()
    {
        return new EnemyObjectSoundState(context,this);
    }

    public EnemyPlayerSoundState PFSound()
    {
        return new EnemyPlayerSoundState(context,this); 
    }

    public EnemyWinState GameLost()
    {
        GameManager.current.Die = true;
        return new EnemyWinState(context, this);
    }

}
