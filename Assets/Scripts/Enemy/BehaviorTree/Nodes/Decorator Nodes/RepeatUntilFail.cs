public class RepeatUntilFail : ADecoratorNode
{
    public RepeatUntilFail() : base() { }

    public override NodeState EvaluateFromChildNode(ANode childNode)
    {
        switch (childNode.Evaluate())
        {
            case NodeState.FAILURE:
                return NodeState.FAILURE;
            default:
                return NodeState.RUNNING;
        }
    }
}