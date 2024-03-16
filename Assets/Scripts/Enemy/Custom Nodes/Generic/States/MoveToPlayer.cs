using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : StateNode
{
    private bool _movementFailed = false;

    public MoveToPlayer(float cooldown = 0) : base(cooldown) { }

    public override void UpdateState()
    {
        Debug.Log("Currently moving to player");
        _movementFailed = !enemyController.TryMoveToPosDirect(playerInfo.GetPos());
    }

    public override bool FailConditionMet()
    {
        return _movementFailed;
    }

    public override bool SuccessConditionMet()
    {
        return enemyInfo.PosReached(playerInfo.GetPos());
    }

    public override void OnEnterState()
    {
        Debug.Log("Attempting to move to player");
    }

    public override void OnExitState()
    {
        enemyController.StopMovement();
        Debug.Log("Move to player finished");
    }

    public override void OnSuccess()
    {
        Debug.Log("Moved to player Successfully");
    }

    public override void OnFailure()
    {
        Debug.Log("Failed on moving to player");
    }
}

