public class StandardSelector : ASelector
{
    public override void ProcessNode()
    {
        Operate(children);
    }
}