using System.Collections.Generic;

public class TestBoss1Tree : ABehaviorTree
{
    protected override ANode SetupTree()
    {
        return new StandardSelector(new List<ANode>()
        {
            new Boss1MeleeAttack("AttackPattern1", 0.6f, 0.7f, 20, 100, new List<AConditionNode>()
            {
                new PlayerInRange(10)
            }),
            new StandardSequence(new List<ANode>()
            {
                //new BoneSpurs("Bone Spikes", 0.43f, new List<AConditionNode>{
                //    new PlayerOutOfRange(10)
                //})
                new PlayerOutOfRange(10),
                new MoveToPlayer()
            })
        });
    }
}