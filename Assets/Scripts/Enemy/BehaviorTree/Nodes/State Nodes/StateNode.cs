using Unity.VisualScripting;

public class StateNode : ANode
{
    private bool _active;
    private float _cooldown;

    public StateNode(float cooldown = 0) : base()
    {
        _cooldown = cooldown;
    }

    public override void InitNode()
    {
        _active = false;
    }

    public override void ProcessNode()
    {
        if (!_active)
        {
            if (_cooldown > 0)
            {
                UnityEngine.Debug.Log("%% SETTING COOLDOWN");
                enemyInfo.SetStateCooldown(this, _cooldown);
            }
            SetupState();
            if (!enemyInfo.CanPerformState(this) || !CanRun())
            {
                SetState(NodeState.FAILURE);
                return;
            }
            _active = true;
            enemyInfo.SetCurrentStateName(GetType().Name);
            enemyInfo.LogState(this);
            OnEnterState();
        }
        SetState(NodeState.RUNNING);
        UpdateState();
        ProcessExitConditions();
    }

    public override void OnExitNode()
    {
        enemyInfo.SetCurrentStateName("NONE");
        _active = false;
        OnExitState();
    }

    public virtual void SetupState() { }

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

    public virtual bool CanRun()
    {
        return true;
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
