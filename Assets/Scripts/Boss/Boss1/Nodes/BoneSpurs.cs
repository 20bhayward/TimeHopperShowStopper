using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpurs : ADelayedAnimationEventNode
{
    public BoneSpurs(string animationStateName, float eventDelay, List<AConditionNode> conditions = null) : base(animationStateName, eventDelay, conditions)
    {
    }

    public override void OnEnterState()
    {
        enemyController.RotateTowardPos(playerInfo.GetPos());
    }

    public override void PerformDelayedEvent()
    {
        Boss1Controller bossController = (Boss1Controller)enemyController;
        bossController.FireBoneSpurs(playerInfo.GetPos());
    }
}
