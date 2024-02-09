public class SelectRandomIndefinite : ATrueRandomCompositeNode
{
    public SelectRandomIndefinite() : base() { }

    public override void ProcessNode()
    {
        ANode randomChild = GetRandomChild();
        randomChild.Evaluate();
        SetState(NodeState.RUNNING);
    }
}