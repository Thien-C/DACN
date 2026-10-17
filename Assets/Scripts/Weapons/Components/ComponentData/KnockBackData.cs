namespace Hung.Weapons.Components
{
    public class KnockBackData : ComponentData<AttackKnockBack>
    {
        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(KnockBack);
        }
    }
}
