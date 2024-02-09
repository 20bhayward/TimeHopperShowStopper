public class RandomSequence : ASequence
{
    public RandomSequence() : base() { }

    public override void ProcessNode()
    {
        Operate(RandomizeChildren());
    }
}