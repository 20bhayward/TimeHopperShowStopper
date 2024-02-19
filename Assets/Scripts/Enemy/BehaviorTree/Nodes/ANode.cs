using System.Collections.Generic;

public abstract class ANode
{
    private NodeState _state;
    protected ANode parent;
    protected List<ANode> children;
    protected EnemyController enemyController;
    protected EnemyInfoManager enemyInfo;
    protected PlayerInfoManager playerInfo;
    protected WorldInfoManager worldInfo;

    public ANode()
    {
        parent = null;
        children = new List<ANode> { };
        InitNode();
    }

    public ANode(List<ANode> children)
    {
        children = new List<ANode> { };
        foreach (ANode child in children)
        {
            Attach(child);
        }
        InitNode();
    }

    public NodeState Evaluate()
    {
        ProcessNode();
        return _state;
    }

    public abstract void ProcessNode();

    public virtual void InitNode() { }

    public virtual void OnExitToParent() { }

    public virtual void OnFailure() { }

    public virtual void OnSuccess() { }

    public void SetState(NodeState state)
    {
        if (_state == state)
        {
            return;
        }
        _state = state;
        if (_state != NodeState.RUNNING)
        {
            if (_state == NodeState.FAILURE)
            {
                enemyController.StopAllCoroutines();
                OnFailure();
            }
            else
            {
                enemyController.StopAllCoroutines();
                OnSuccess();
            }

            ExitAllChildren();
        }
    }

    public void SetParent(ANode parent)
    {
        this.parent = parent;
    }

    public void SetControllerAndInfo(EnemyController enemyController, EnemyInfoManager enemyInfo, PlayerInfoManager playerInfo, WorldInfoManager worldInfo)
    {
        this.enemyController = enemyController;
        this.enemyInfo = enemyInfo;
        this.playerInfo = playerInfo;
        this.worldInfo = worldInfo;
        foreach (ANode node in children)
        {
            node.SetControllerAndInfo(enemyController, enemyInfo, playerInfo, worldInfo);
        }
    }

    private void Attach(ANode node)
    {
        node.SetParent(this);
        children.Add(node);
    }

    private void ExitAllChildren()
    {
        foreach (ANode node in children)
        {
            node.OnExitToParent();
            node.SetState(_state);
        }
    }
}