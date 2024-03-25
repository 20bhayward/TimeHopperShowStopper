using System.Collections.Generic;
using System.Diagnostics;

public class FiniteAnimationNode : StateNode
{
    protected string animationStateName;
    protected List<AConditionNode> conditions;

    public FiniteAnimationNode(string animationStateName, List<AConditionNode> conditions = null, float cooldown = 0, float delay = 0) : base(cooldown, delay)
    {
        this.animationStateName = animationStateName;
        if (conditions == null)
        {
            this.conditions = new List<AConditionNode>();
        }
        this.conditions = conditions;
    }

    public override void SetupState()
    {
        SetupConditions();
    }

    public override bool CanRun()
    {
        return AllConditionsPass();
    }

    public override void OnEnterState()
    {
        enemyController.PlayAnimationState(animationStateName);
    }

    public override bool SuccessConditionMet()
    {
        return enemyController.GetCurrentAnimationStateProgress() >= 1f;
    }

    public override bool FailConditionMet()
    {
        return SuccessConditionMet() && !AllConditionsPass();
    }

    public override void OnExitState()
    {
        //UnityEngine.Debug.Log("^^EXITING STATE");
        enemyController.ReturnToIdleState();
    }

    private void SetupConditions()
    {
        foreach (AConditionNode node in conditions)
        {
            node.SetControllerAndInfo(enemyController, enemyInfo, playerInfo, worldInfo);
        }
    }

    private bool AllConditionsPass()
    {
        foreach (AConditionNode node in conditions)
        {
            if (!node.ConditionTrue())
            {
                return false;
            }
        }
        return true;
    }
}