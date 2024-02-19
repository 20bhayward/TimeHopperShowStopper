using System.Collections.Generic;
public class StandardSelector : ASelector
{
    public StandardSelector(List<ANode> children) : base(children) { }

    public override void ProcessNode()
    {
        Operate(children);
    }
}