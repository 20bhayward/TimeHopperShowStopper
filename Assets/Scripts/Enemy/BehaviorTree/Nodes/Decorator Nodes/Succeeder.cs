public class Succeeder : ADecoratorNode
{
    public override NodeState EvaluateFromChildNode(ANode childNode)
    {
        switch (childNode.Evaluate())
        {
            case (NodeState.RUNNING):
                return NodeState.RUNNING;
            default:
                return NodeState.SUCCESS;
        }
    }
}