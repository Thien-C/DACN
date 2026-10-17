using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hung.Weapons.Components
{
    public class MovementData : ComponentData<AttackMovement>
    {
        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(MovementAttack);
        }
    }
}

