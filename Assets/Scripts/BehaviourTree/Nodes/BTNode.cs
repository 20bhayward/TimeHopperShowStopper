using System.Collections.Generic;

public class BTNode
{
    protected BTNodeState state;
    protected BTNode parent;
    protected List<BTNode> children;

    public BTNode()
    {
        parent = null;
    }

    public BTNode(List<BTNode> children)
    {
        foreach (BTNode child in children)
        {
            Attach(child);
        }
    }

    public virtual BTNodeState Evaluate()
    {
        return BTNodeState.FAILURE;
    }

    public void SetParent(BTNode parent)
    {
        this.parent = parent;
    }

    private void Attach(BTNode node)
    {
        node.SetParent(this);
        children.Add(node);
    }
}