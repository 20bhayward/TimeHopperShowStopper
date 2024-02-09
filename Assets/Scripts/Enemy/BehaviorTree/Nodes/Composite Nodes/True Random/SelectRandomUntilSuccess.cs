public class SelectRandomUntilSuccess : ATrueRandomCompositeNode
{
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