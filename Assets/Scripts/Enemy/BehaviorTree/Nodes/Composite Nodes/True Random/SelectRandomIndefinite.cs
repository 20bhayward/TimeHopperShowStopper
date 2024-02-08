public class SelectRandomIndefinite : ATrueRandomCompositeNode
{
    public override void ProcessNode()
    {
        ANode randomChild = GetRandomChild();
        randomChild.Evaluate();
        SetState(NodeState.RUNNING);
    }
}