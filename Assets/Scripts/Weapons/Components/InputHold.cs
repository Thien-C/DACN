using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hung.Weapons.Components
{
    public class InputHold : WeaponComponent
    {
        private Animator anim;

        private bool input;

        private bool minHoldPassed;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            minHoldPassed = false;
        }
        private void HanddleCurrentInputChange(bool newInput)
        {
            input = newInput;
            SetAnimatorParamater();
        }
        private void SetAnimatorParamater()
        {
            if (input)
            {
                anim.SetBool("hold", input);
                return;
            }

            if (minHoldPassed)
            {
                anim.SetBool("hold", false);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            anim = GetComponentInChildren<Animator>();

            weapon.OnCurrentInputChange += HanddleCurrentInputChange;
            eventHandler.OnMinHoldPassed += HandleMinHoldPaassed;
        }
        private void HandleMinHoldPaassed()
        {

            minHoldPassed = true;
            SetAnimatorParamater();
        }
        protected override void OnDesTroy()
        {
            base.OnDesTroy();

            weapon.OnCurrentInputChange -= HanddleCurrentInputChange;
            eventHandler.OnMinHoldPassed -= HandleMinHoldPaassed;
        }
    }
}

