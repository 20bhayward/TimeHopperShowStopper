using System.Collections.Generic;

public abstract class ASelector : AOrderedCompositeNode
{
    protected void Operate(List<ANode> childNodes)
    {
        foreach (ANode node in childNodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    SetState(NodeState.SUCCESS);
                    return;
                case NodeState.RUNNING:
                    SetState(NodeState.RUNNING);
                    return;
                default:
                    continue;
            }
        }

        SetState(NodeState.FAILURE);
    }
}