using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hung.Weapons.Components
{
    public class InputHoldData : ComponentData
    {
        protected override void SetComponentDepedency()
        {
            ComponentDependecny = typeof(InputHold);
        }
    }

}
