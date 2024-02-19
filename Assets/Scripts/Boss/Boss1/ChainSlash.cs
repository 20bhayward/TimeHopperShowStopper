using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSlash : AStateNode
{
    public override void UpdateState()
    {
        Debug.Log("Currently Firing Chain Slash");
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
        Debug.Log("Firing Chain Slash");
    }

    public override void OnExitState()
    {
        Debug.Log("Attack Finished");
    }

    public override void OnSuccess()
    {
        Debug.Log("Chain Slash fired Successfully");
    }

    public override void OnFailure()
    {
        Debug.Log("Something went wrong, I failed Chain Slash");
    }
}
