namespace Hung.Weapons.Components
{
    public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
    {
        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(ProjectileSpawner);
        }

    }
}