using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1PlayerinRange : AConditionNode
{
    public override bool ConditionTrue()
    {
        return (enemyInfo.GetDistanceTo(playerInfo.GetPos()) < 500f);
    }
}
