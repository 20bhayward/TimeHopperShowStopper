using System.Collections.Generic;

public class TestBossAttackTree : ABehaviorTree
{
    protected override ANode SetupTree()
    {
        return new StandardSelector(new List<ANode>()
        {
            new FiniteAnimationNode("AttackPattern1", new List<AConditionNode>()
            {
                new PlayerInRange(10)
            }),
            new StandardSequence(new List<ANode>()
            {
                new PlayerOutOfRange(10),
                new MoveToPlayer()
            })
        });
    }
}