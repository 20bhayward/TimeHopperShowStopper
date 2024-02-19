using System.Collections.Generic;
public class RandomSelector : ASelector
{
    public RandomSelector(List<ANode> children) : base(children) { }

    public override void ProcessNode()
    {
        Operate(RandomizeChildren());
    }
}