using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : AConditionNode
{
    private int range;

    // Constructor taking an integer parameter
    public PlayerInRange(int range)
    {
        this.range = range;
    }

    // Override ConditionTrue method
    public override bool ConditionTrue()
    {
        // Assuming enemyInfo and playerInfo are accessible variables
        return (enemyInfo.GetDistanceTo(playerInfo.GetPos()) < range);
    }
}
