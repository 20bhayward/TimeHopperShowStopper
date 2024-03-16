using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeWave : ADelayedAnimationEventNode
{
    public NegativeWave(string animationStateName, float eventDelay, List<AConditionNode> conditions = null, float cooldown = 0) 
        : base(animationStateName, eventDelay, conditions, cooldown)
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
        //bossController.FireNegativeWave(playerInfo.GetPos());
    }
}
