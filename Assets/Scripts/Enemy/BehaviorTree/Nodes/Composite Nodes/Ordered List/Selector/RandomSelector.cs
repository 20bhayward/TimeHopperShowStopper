public class RandomSelector : ASelector
{
    public override void ProcessNode()
    {
        Operate(RandomizeChildren());
    }
}