using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SelectRandomIndefinite : ATrueRandomCompositeNode
{

    public SelectRandomIndefinite(List <ANode> children) : base(children) { }

    public override void ProcessNode()
    {
        ANode randomChild = GetRandomChild();
        randomChild.Evaluate();
        SetState(NodeState.RUNNING);
    }
}