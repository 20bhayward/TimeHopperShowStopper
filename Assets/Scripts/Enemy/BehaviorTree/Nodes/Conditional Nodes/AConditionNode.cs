public abstract class AConditionNode : ANode
{
    public override void ProcessNode()
    {
        if (ConditionTrue())
        {
            SetState(NodeState.SUCCESS);
        } 
        else
        {
            SetState(NodeState.FAILURE);
        }
    }

    public abstract bool ConditionTrue();
}