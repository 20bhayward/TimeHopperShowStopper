public class FiniteAnimationNode : StateNode
{
    protected string animationStateName;

    public FiniteAnimationNode(string animationStateName) : base()
    {
        this.animationStateName = animationStateName;
    }

    public override void OnEnterState()
    {
        enemyController.PlayAnimationState(animationStateName);
    }

    public override bool SuccessConditionMet()
    {
        return enemyController.GetCurrentAnimationStateProgress() >= 1f;
    }

    public override void OnFailure()
    {
        enemyController.ExitCurrentAnimationState();
    }
}