public class AnimatorBusy : AConditionNode
{
    public override bool ConditionTrue()
    {
        return enemyController.GetCurrentAnimationStateProgress() < 1f;
    }
}
