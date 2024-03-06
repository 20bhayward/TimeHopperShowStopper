using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASequence : AOrderedCompositeNode
{
    public ASequence(List<ANode> children) : base(children) { }
    protected void Operate(List<ANode> childNodes)
    {
        bool anyChildIsRunning = false;
        foreach (ANode node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    if (node.GetType().Name == "PlayerInRange")
                    {
                        Debug.Log("^^PlayerInRange FAILED");
                    }
                    SetState(NodeState.FAILURE);
                    return;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    //SetState(NodeState.RUNNING);
                    //return;
                    anyChildIsRunning = true;
                    continue;
                default:
                    continue;
            }
        }

        SetState(anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS);
        //SetState(NodeState.SUCCESS);
    }
}