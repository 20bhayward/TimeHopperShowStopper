public class AnimatorFree : AConditionNode
{
    public override bool ConditionTrue()
    {
        return enemyController.GetCurrentAnimationStateProgress() >= 1.0f;
    }
}
