public class StandardSequence : ASequence
{
    public StandardSequence() : base() { }
    
    public override void ProcessNode()
    {
        Operate(children);
    }
}