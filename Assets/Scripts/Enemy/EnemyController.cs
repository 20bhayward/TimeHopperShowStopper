using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] private string _idleAnimStateName;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected AEnemyMovement movement;

    protected EnemyInfoManager enemyInfo;
    protected PlayerInfoManager playerInfo;
    protected WorldInfoManager worldInfo;

    private bool _navigating = false;

    public void SetInfo(EnemyInfoManager enemyInfo, PlayerInfoManager playerInfo, WorldInfoManager worldInfo)
    {
        this.enemyInfo = enemyInfo;
        this.playerInfo = playerInfo;
        this.worldInfo = worldInfo;
    }

    #region RIGIDBODY METHODS
    public Rigidbody GetRigidbody()
    { 
        return rb; 
    }
    #endregion

    #region MOVEMENT METHODS
    public void RotateTowardPos(Vector3 pos)
    {
        movement.RotateTowardPos(pos);
    }

    public void MoveToPosDirect(Vector3 pos)
    {
        movement.MoveTowardPos(pos);
    }

    public void StopMovement()
    {
        movement.StopMovement();
    }

    public bool TryMoveToPosDirect(Vector3 pos)
    {
        if (enemyInfo.CanMoveToPosDirect(pos))
        {
            MoveToPosDirect(pos);
            return true;
        }
        movement.StopMovement();
        return false;
        
    }

    protected bool TryMoveToPosOnNavMesh(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(enemyInfo.GetPos(), pos, NavMesh.AllAreas, path))
        {
            if (!_navigating)
            {
                _navigating = true;
                StartCoroutine(MoveAlongPath(path));
            }
            return true;
        }
        _navigating = false;
        return false;
    }

    private IEnumerator MoveAlongPath(NavMeshPath path)
    {
        Vector3[] corners = path.corners;
        int i = 0;
        while (i < corners.Length)
        {
            Vector3 nextCorner = corners[i];
            movement.MoveTowardPos(nextCorner);
            if (enemyInfo.PosReached(nextCorner))
            {
                i++;
            }
            yield return null;
        }
        _navigating = false;
        movement.StopMovement();
    }
    #endregion

    #region ANIMATION METHODS
    public void PlayAnimationState(string animationStateName)
    {
        animator.Play(animationStateName);
    }

    public void ReturnToIdleState()
    {
        Debug.Log("% Returning to Idle");
        animator.Play(_idleAnimStateName);
    }

    public float GetCurrentAnimationStateProgress()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    #endregion
}
