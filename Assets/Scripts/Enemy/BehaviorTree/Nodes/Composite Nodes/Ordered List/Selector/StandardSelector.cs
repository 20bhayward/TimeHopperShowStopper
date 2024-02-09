public class StandardSelector : ASelector
{
    public StandardSelector() : base() { }

    public override void ProcessNode()
    {
        Operate(children);
    }
}