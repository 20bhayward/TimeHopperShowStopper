using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;

public abstract class ANode
{
    private NodeState _state;
    protected ANode parent;
    protected List<ANode> children;
    protected EnemyController enemyController;
    protected EnemyInfoManager enemyInfo;
    protected PlayerInfoManager playerInfo;
    protected WorldInfoManager worldInfo;

    private Action _exitBehavior = null;

    public ANode()
    {
        parent = null;
        children = new List<ANode> { };
        InitNode();
    }

    public ANode(List<ANode> children)
    {
        this.children = new List<ANode> { };
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

    public NodeState GetState()
    {
        return _state;
    }

    public abstract void ProcessNode();

    public virtual void InitNode() { }

    public virtual void OnExitNode() { }

    public virtual void OnFailure() { }

    public virtual void OnSuccess() { }

    public void SetState(NodeState state)
    {
        if (_state == state)
        {
            return;
        }
        NodeState nextState = state;
        if (nextState != NodeState.RUNNING)
        {
            ProcessStateTransition(nextState);
        }
        else
        {
            _state = nextState;
        }
    }

    private void ProcessStateTransition(NodeState nextState)
    {
        _state = nextState;
        if (_state == NodeState.FAILURE)
        {
            enemyController.StopAllCoroutines();
            OnExitNode();
            OnFailure();
        }
        else
        {
            enemyController.StopAllCoroutines();
            OnExitNode();
            OnSuccess();
        }
        ExitAllChildren();
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
            /*if (node.GetType().Name == "FiniteAnimationNode")
            {
                UnityEngine.Debug.Log("^^Exiting FiniteAnimationNode child");
            }*/
            node.OnExitNode();
            node.SetState(_state);
        }
    }
}