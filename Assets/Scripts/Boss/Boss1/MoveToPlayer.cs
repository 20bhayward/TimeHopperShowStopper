using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : StateNode
{
    public override void UpdateState()
    {
        Debug.Log("Currently moving to player");
        enemyController.TryMoveToPosDirect(playerInfo.GetPos());
    }

    public override bool FailConditionMet()
    {
        return false;
    }

    public override bool SuccessConditionMet()
    {
        return true;
    }

    public override void OnEnterState()
    {
        Debug.Log("Attempting to move to player");
    }

    public override void OnExitState()
    {
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

