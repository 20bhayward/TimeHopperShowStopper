using System.Collections.Generic;
public class SelectRandomUntilFailure : ATrueRandomCompositeNode
{
    public SelectRandomUntilFailure(List<ANode> children) : base(children) { }

    public override void ProcessNode()
    {
        ANode randomChild = GetRandomChild();
        switch (randomChild.Evaluate())
        {
            case NodeState.FAILURE:
                SetState(NodeState.FAILURE);
                return;
            default:
                SetState(NodeState.RUNNING);
                return;
        }
    }
}