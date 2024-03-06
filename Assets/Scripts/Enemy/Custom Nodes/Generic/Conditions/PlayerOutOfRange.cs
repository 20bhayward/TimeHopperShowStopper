using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfRange : AConditionNode
{
    private int range;

    public PlayerOutOfRange(int range)
    {
        this.range = range;
    }

    public override bool ConditionTrue()
    {       
        return (enemyInfo.GetDistanceTo(playerInfo.GetPos()) >= range);
    }
}
