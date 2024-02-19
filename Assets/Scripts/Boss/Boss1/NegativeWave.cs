using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeWave : AStateNode
{
    public override void UpdateState()
    {
        Debug.Log("Currently Firing Negative Wave");
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
        Debug.Log("Firing Negative Wave");
    }

    public override void OnExitState()
    {
        Debug.Log("Attack Finished");
    }

    public override void OnSuccess()
    {
        Debug.Log("Negative Wave fired Successfully");
    }

    public override void OnFailure()
    {
        Debug.Log("Something went wrong, I failed Negative Wave");
    }
}
