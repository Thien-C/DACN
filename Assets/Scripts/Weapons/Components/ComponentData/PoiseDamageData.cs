namespace Hung.Weapons.Components
{
    public class PoiseDamageData : ComponentData<AttackPoiseDamage>
    {
        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(PoiseDamage);
        }
    }

}
