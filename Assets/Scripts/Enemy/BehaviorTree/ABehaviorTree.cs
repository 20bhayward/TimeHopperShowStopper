using UnityEngine;

public abstract class ABehaviorTree : MonoBehaviour
{
    private ANode _root;

    public virtual void Start()
    {
        _root = SetupTree();
    }

    public virtual void Update()
    {
        if (_root != null)
        {
            _root.Evaluate();
        }
    }

    protected abstract ANode SetupTree();
}