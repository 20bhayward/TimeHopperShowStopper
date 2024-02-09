public class StandardSequence : ASequence
{
    StandardSequence() : base() { }
    
    public override void ProcessNode()
    {
        Operate(children);
    }
}