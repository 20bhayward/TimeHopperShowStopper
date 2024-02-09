public class SelectRandomUntilFailure : ATrueRandomCompositeNode
{
    public SelectRandomUntilFailure() : base() { }

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