public class RandomSequence : ASequence
{
    public override void ProcessNode()
    {
        Operate(RandomizeChildren());
    }
}