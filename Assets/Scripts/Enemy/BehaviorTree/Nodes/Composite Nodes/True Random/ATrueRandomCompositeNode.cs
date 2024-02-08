using UnityEngine;

public abstract class ATrueRandomCompositeNode : ANode
{
    public ANode GetRandomChild()
    {
        return children[Random.Range(0, children.Count - 1)];
    }
}