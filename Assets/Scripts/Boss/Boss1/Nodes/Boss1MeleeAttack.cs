using System.Collections.Generic;
using UnityEngine;

public class Boss1MeleeAttack : FiniteAnimationNode
{
    private float _damage;
    private float _forwardMovement;
    private float _startForwardMovement;
    private float _endForwardMovement;
    private bool _movementStarted = false;
    private bool _movementFinished = false;

    public Boss1MeleeAttack(string animationStateName, float startForwardMovement, float endForwardMovement, float damage, float forwardMovement, 
        List<AConditionNode> conditions = null) : base(animationStateName, conditions)
    {
        _damage = damage;
        _forwardMovement = forwardMovement;
        _startForwardMovement = startForwardMovement;
        _endForwardMovement = endForwardMovement;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        Boss1Controller bossController = (Boss1Controller)enemyController;
        bossController.StartMeleeAttack(_damage);
        enemyController.RotateTowardPos(playerInfo.GetPos());
        _movementStarted = false;
        _movementFinished = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (enemyController.GetCurrentAnimationStateProgress() >= _startForwardMovement && !_movementStarted)
        {
            _movementStarted = true;
            Rigidbody rb = enemyController.GetRigidbody();
            rb.AddForce(rb.transform.forward * _forwardMovement, ForceMode.Impulse);
        }
        if (enemyController.GetCurrentAnimationStateProgress() >= _endForwardMovement && !_movementFinished)
        {
            _movementFinished = true;
            enemyController.GetRigidbody().velocity = Vector3.zero;
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();
        Boss1Controller bossController = (Boss1Controller)enemyController;
        bossController.EndMeleeAttack();
    }
}
