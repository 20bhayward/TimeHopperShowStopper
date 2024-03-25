using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpurs : ADelayedAnimationEventNode
{
    public BoneSpurs(string animationStateName, float eventDelay, List<AConditionNode> conditions = null,
        float cooldown = 0, float delay = 0) : base(animationStateName, eventDelay, conditions, cooldown, delay)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        enemyController.RotateTowardPos(playerInfo.GetPos());
    }

    public override void PerformDelayedEvent()
    {
        Boss1Controller bossController = (Boss1Controller)enemyController;
        bossController.FireBoneSpurs(playerInfo.GetPos());
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}
