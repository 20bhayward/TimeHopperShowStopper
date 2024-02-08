using System;

public abstract class ADecoratorNode : ANode
{
    public override void ProcessNode()
    {
        if (children.Count != 1)
        {
            throw new Exception("A Decorator Node must have exactly one child node!");
        }

        SetState(EvaluateFromChildNode(children[0]));
    }

    public abstract NodeState EvaluateFromChildNode(ANode childNode);
}