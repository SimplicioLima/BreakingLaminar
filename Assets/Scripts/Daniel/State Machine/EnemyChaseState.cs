using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory) :
        base(currentContext, enemyStateFactory)
    { }


    const float pathUpdateMoveThreshold = 0.5f;
    const float minPathUpdateTime = 0.2f;


    public override void EnterState()
    {
        Debug.Log("Chasing Player!");
        _ctx.StartCoroutine(UpdatePath());

    }

    public override void ExitState()
    {
        _ctx.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (!_ctx.IsPlayerVisible)
            SwitchState(_factory.Patrol());

        if(Vector3.Distance(_ctx.Enemy.position,_ctx.Player.position) < Mathf.Pow(2f,2f)) 
        {
            _ctx.StopAllCoroutines();
            Debug.Log("Game Over!");
        }

    }


    public void OnPathFound(Vector3[] waypoints, bool success)
    {
        if (success)
        {
            _ctx.Path = new Path(waypoints, _ctx.Enemy.position, _ctx.TurningDistance, _ctx.StoppingDistance);
            _ctx.StopCoroutine(FollowPlayer());
            _ctx.StartCoroutine(FollowPlayer());
        }
    }


    IEnumerator FollowPlayer()
    {
        int pathIndex = 0;
        _ctx.Enemy.LookAt(_ctx.Path.lookPoints[0]);

        float speedPercent = 1;

        _ctx.FollowingPath = true;
        while (_ctx.FollowingPath)
        {
            Vector2 pos2D = new Vector2(_ctx.Enemy.position.x, _ctx.Enemy.position.z);
            while (_ctx.Path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == _ctx.Path.finishLineIndex)
                {
                    _ctx.FollowingPath = false;
                    break;
                }
                else
                    pathIndex++;
            }
            if (_ctx.FollowingPath)
            {
                if (pathIndex >= _ctx.Path.slowDownIndex && _ctx.StoppingDistance > 0)
                {
                    speedPercent = Mathf.Clamp01(_ctx.Path.turnBoundaries[_ctx.Path.finishLineIndex].DistanceFromPoint(pos2D) / _ctx.StoppingDistance);
                    if (speedPercent < 0.01f)
                        _ctx.FollowingPath = false;
                }

                Quaternion targetRotation = Quaternion.LookRotation(_ctx.Path.lookPoints[pathIndex] - _ctx.Enemy.position);
                _ctx.Enemy.rotation = Quaternion.Lerp(_ctx.Enemy.rotation, targetRotation, _ctx.TurnSpeed * Time.deltaTime);
                float yPosition = _ctx.Enemy.position.y;
                yPosition = 1;
                _ctx.Enemy.Translate(_ctx.Speed * speedPercent * Vector3.forward * Time.deltaTime);
            }

            yield return null;
        }

    }


    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        { 
            yield return new WaitForSeconds(minPathUpdateTime);
        }
        PathManager.RequestPath(new PathRequest(_ctx.Enemy.position, _ctx.Player.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPositionOld = _ctx.Player.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((_ctx.Player.position - targetPositionOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathManager.RequestPath(new PathRequest(_ctx.Enemy.position, _ctx.Player.position, OnPathFound));
                targetPositionOld = _ctx.Player.position;
            }
        }
    }



}
