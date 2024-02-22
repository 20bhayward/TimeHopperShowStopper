using System.Collections.Generic;

public class TestBossAttackTree : ABehaviorTree
{
    protected override ANode SetupTree()
    {
        return new StandardSelector(new List<ANode>()
        {
            new StandardSequence(new List<ANode>()
            {
                new PlayerInRange(50),
                new FiniteAnimationNode("AttackPattern1")
            }),
            new StateNode()
        });
    }
}