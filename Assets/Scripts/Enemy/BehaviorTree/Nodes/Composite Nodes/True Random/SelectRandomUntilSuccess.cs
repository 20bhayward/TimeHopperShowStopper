using System.Collections.Generic;
public class SelectRandomUntilSuccess : ATrueRandomCompositeNode
{
    public SelectRandomUntilSuccess(List<ANode> children) : base(children) { }

    public override void ProcessNode()
    {
        ANode randomChild = GetRandomChild();
        switch (randomChild.Evaluate())
        {
            case NodeState.SUCCESS:
                SetState(NodeState.SUCCESS);
                return;
            default:
                SetState(NodeState.RUNNING);
                return;
        }
    }
} 