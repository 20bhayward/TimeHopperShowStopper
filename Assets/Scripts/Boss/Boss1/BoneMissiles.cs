using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMissiles : AStateNode
{

    public override void UpdateState()
    {
        Debug.Log("Currently Firing Bone Missiles");
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
        Debug.Log("Firing Bone Missiles!");
    }

    public override void OnExitState()
    {
        Debug.Log("Attack Finished");
    }

    public override void OnSuccess()
    {
        Debug.Log("Bone Missiles fired Successfully");
    }

    public override void OnFailure()
    {
        Debug.Log("Something went wrong, I failed firing Bone Missiles");
    }

}
