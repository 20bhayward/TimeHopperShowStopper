using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StandardSequence : ASequence
{

    public StandardSequence(List<ANode> children) : base(children) { }

    public override void ProcessNode()
    {
        Operate(children);
    }
}