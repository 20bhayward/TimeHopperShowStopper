using System.Collections.Generic;
using UnityEngine;

public abstract class AOrderedCompositeNode : ANode
{

    public AOrderedCompositeNode(List<ANode> children) : base(children) { }
    public List<ANode> RandomizeChildren()
    {
        List<int> indexes = new List<int>();
        List<ANode> returnList = new List<ANode>();

        while (returnList.Count < children.Count)
        {
            int i = Random.Range(0, children.Count - 1);
            if (!indexes.Contains(i))
            {
                returnList.Add(children[i]);
            }
        }

        return returnList;
    }
}