public class StandardSequence : ASequence
{
    public override void ProcessNode()
    {
        Operate(children);
    }
}