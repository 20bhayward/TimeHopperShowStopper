public class Repeater : ADecoratorNode
{
    private uint _repetitions;
    private bool _repeatIndefinitely;

    public Repeater(): base()
    {
        _repeatIndefinitely = true;
    }

    public Repeater(uint repetitions)
    {
        _repetitions = repetitions;
        _repeatIndefinitely = false;
    }

    public override NodeState EvaluateFromChildNode(ANode childNode)
    {
        if (_repeatIndefinitely)
        {
            childNode.Evaluate();
            return NodeState.RUNNING;
        }
        else
        {
            NodeState childState = childNode.Evaluate();
            switch (childState)
            {
                case NodeState.RUNNING:
                    return NodeState.RUNNING;
                default:
                    return NodeStateAfterNextRepetition(childState);
            }
        }
    }

    private NodeState NodeStateAfterNextRepetition(NodeState childState)
    {
        _repetitions--;
        if (_repetitions == 0)
        {
            return childState;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
}