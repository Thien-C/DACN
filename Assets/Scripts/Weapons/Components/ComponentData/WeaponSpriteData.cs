using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hung.Weapons.Components
{
    public class WeaponSpriteData : ComponentData<AttackSprites>
    {
        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(WeaponSprite);
        }
    }
}

