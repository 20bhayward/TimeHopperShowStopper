public class TemplateState : AStateNode
{
    public TemplateState() : base() { }

    public override void UpdateState()
    {
        
    }

    // VIRTUAL
    public override bool FailConditionMet()
    {
        return false;
    }

    public override bool SuccessConditionMet()
    {
        return false;
    }

    public override void OnEnterState()
    {
        
    }

    public override void OnExitState()
    {
        
    }

    public override void OnSuccess()
    {
        
    }

    public override void OnFailure()
    {
        
    }
}