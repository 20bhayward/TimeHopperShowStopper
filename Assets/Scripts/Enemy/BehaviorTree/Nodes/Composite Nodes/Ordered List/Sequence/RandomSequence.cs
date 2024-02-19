using System.Collections.Generic;
public class RandomSequence : ASequence
{

    public RandomSequence(List<ANode> children) : base(children) { }
    public override void ProcessNode()
    {
        Operate(RandomizeChildren());
    }
}