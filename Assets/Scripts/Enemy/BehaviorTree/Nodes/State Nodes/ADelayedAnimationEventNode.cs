public abstract class ADelayedAnimationEventNode : FiniteAnimationNode
{
    protected float eventDelay;

    public ADelayedAnimationEventNode(string animationStateName, float eventDelay) : base(animationStateName)
    {
        if (eventDelay < 0 || eventDelay > 1)
        {
            throw new System.ArgumentOutOfRangeException("Event delay must be between 0 and 1!");
        }
        this.eventDelay = eventDelay;
    }

    public override void UpdateState()
    {
        if (enemyController.GetCurrentAnimationStateProgress() >= eventDelay)
        {
            PerformDelayedEvent();
        }
    }

    public abstract void PerformDelayedEvent();
}