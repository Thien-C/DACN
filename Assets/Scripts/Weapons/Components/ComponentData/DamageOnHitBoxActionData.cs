using Hung.Weapons.Components;

namespace Hung.Weapons.Components
{
    public class DamageOnHitBoxActionData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(DamageOnHitBoxAction);

        }
    }
}