using System.Collections.Generic;

public abstract class ASequence : AOrderedCompositeNode
{
    protected void Operate(List<ANode> childNodes)
    {
        bool anyChildIsRunning = false;
        foreach (ANode node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    SetState(NodeState.FAILURE);
                    return;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    anyChildIsRunning = true;
                    continue;
                default:
                    SetState(NodeState.SUCCESS);
                    return;
            }
        }

        SetState(anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS);
    }
}