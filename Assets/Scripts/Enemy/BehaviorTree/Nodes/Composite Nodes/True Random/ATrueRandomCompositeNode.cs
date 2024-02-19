using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATrueRandomCompositeNode : ANode
{

    public ATrueRandomCompositeNode(List<ANode> children) : base(children) { }


    public ANode GetRandomChild()
    {
        return children[Random.Range(0, children.Count - 1)];
    }
}