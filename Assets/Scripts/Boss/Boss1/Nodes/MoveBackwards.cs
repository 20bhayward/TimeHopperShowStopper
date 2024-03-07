using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwards : StateNode
{
    public override void UpdateState()
    {

        float backingUpDistance = 50f;
        Debug.Log("Currently moving backwards");
        enemyController.TryMoveToPosDirect(enemyInfo.transform.position - enemyInfo.transform.forward * backingUpDistance);
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
        Debug.Log("Attempting to back up");
    }

    public override void OnExitState()
    {
        Debug.Log("Backing up finished");
    }

    public override void OnSuccess()
    {
        Debug.Log("Backed Up Successful");
    }

    public override void OnFailure()
    {
        Debug.Log("Failed on Backing up, moving backwards I mean");
    }
}
