public class RandomSelector : ASelector
{
    public RandomSelector() : base() { }

    public override void ProcessNode()
    {
        Operate(RandomizeChildren());
    }
}