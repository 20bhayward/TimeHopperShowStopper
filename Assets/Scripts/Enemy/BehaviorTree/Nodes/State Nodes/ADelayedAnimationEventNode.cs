using System.Collections.Generic;

public abstract class ADelayedAnimationEventNode : FiniteAnimationNode
{
    protected float eventDelay;
    protected bool eventTriggered;

    public ADelayedAnimationEventNode(string animationStateName, float eventDelay,
        List<AConditionNode> conditions = null, float cooldown = 0) : base(animationStateName, conditions, cooldown)
    {
        if (eventDelay < 0 || eventDelay > 1)
        {
            throw new System.ArgumentOutOfRangeException("Event delay must be between 0 and 1!");
        }
        this.eventDelay = eventDelay;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        eventTriggered = false;
    }

    public override void UpdateState()
    {
        UnityEngine.Debug.Log("####" + enemyController.GetCurrentAnimationStateProgress());
        if (enemyController.GetCurrentAnimationStateProgress() >= eventDelay && !eventTriggered)
        {
            eventTriggered = true;
            PerformDelayedEvent();
        }
    }

    public abstract void PerformDelayedEvent();
}