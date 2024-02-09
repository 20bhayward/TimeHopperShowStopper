public class Inverter : ADecoratorNode
{
    public Inverter() : base() { }

    public override NodeState EvaluateFromChildNode(ANode childNode)
    {
        switch (childNode.Evaluate())
        {
            case NodeState.FAILURE:
                return NodeState.FAILURE;
            case NodeState.SUCCESS:
                return NodeState.FAILURE;
            default:
                return NodeState.RUNNING;
        }
    }
}