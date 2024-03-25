using System.Collections.Generic;

public class TestBoss1Tree : ABehaviorTree
{
    protected override ANode SetupTree()
    {
        return new StandardSelector(new List<ANode>()
        {
            new Boss1MeleeAttack("AttackPattern1", 0.6f, 0.7f, 20, 30, new List<AConditionNode>()
            {
                new PlayerInRange(10)
            }),
            new StandardSelector(new List<ANode>()
            {
                new RandomSequence(new List<ANode>()
                {
                    new BoneSpurs("BoneSpikes", 0.43f, new List<AConditionNode>{
                        new PlayerOutOfRange(10)
                    }, 10, 10),
                    /*new NegativeWave("NegativeWave", 0.40f, new List<AConditionNode>
                    {
                        new PlayerOutOfRange(10)
                    }, 6)*/
                }),
                new StandardSequence(new List<ANode>()
                {
                    new PlayerOutOfRange(10),
                    new MoveToPlayer()
                })
            })
        });
    }
}