public class StateNode : ANode
{
    private bool _active;

    public override void InitNode()
    {
        _active = false;
    }

    public override void ProcessNode()
    {
        if (!_active)
        {
            _active = true;
            OnEnterState();
        }
        SetState(NodeState.RUNNING);
        UpdateState();
        ProcessExitConditions();
    }

    public override void OnExitToParent()
    {
        _active = false;
        OnExitState();
    }

    public virtual void UpdateState() { }

    public virtual void OnEnterState() { }

    public virtual void OnExitState() { }

    private void ProcessExitConditions()
    {
        if (FailConditionMet())
        {
            SetState(NodeState.FAILURE);
        }
        if (SuccessConditionMet())
        {
            SetState(NodeState.SUCCESS);
        }
    }

    public virtual bool FailConditionMet()
    {
        return false;
    }

    public virtual bool SuccessConditionMet()
    {
        return false;
    }
}
