public class TemplateCondition : AConditionNode
{
    private int n;

    public TemplateCondition(int n) : base()
    {
        this.n = n;
    }

    public override bool ConditionTrue()
    {
        //return enemyInfo.GetPos() - playerInfo.GetPos() - n;
        return false;
    }
}