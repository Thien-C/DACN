using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hung.Weapons.Components
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }

        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(ActionHitbox);
        }
    }
}

