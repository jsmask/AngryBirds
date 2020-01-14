
public class BuffBird : Bird
{
    public override void showSkill()
    {
        base.showSkill();
        rg.velocity *= 1.5f;
    }
}
