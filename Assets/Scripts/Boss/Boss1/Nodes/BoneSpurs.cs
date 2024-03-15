using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpurs : ADelayedAnimationEventNode
{
    public override void PerformDelayedEvent()
    {
        Boss1Controller bossController = (Boss1Controller)enemyController;
        bossController.FireBoneSpurs(playerInfo.GetPos());
    }
}
