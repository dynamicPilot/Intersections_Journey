
public class VEffectWithView : VEffects
{
    public override void StartEffects()
    {
        base.StartEffects();
        GetComponent<VView>().SetView();
    }
}
