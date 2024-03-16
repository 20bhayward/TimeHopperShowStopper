public class Boss1PastLevelController : ALevelController
{
    public override void DisableLevel()
    {
        gameObject.SetActive(false);
    }

    public override void EnableLevel()
    {
        gameObject.SetActive(true);
    }
}
