using System.Collections.Generic;
using UnityEngine;

public class ex : ABehaviorTree
{
    protected override ANode SetupTree()
    {
        return new RandomSelector(new List<ANode>()
        {
            new RandomSelector(new List<ANode>
            {

            })
        });
    }
}
