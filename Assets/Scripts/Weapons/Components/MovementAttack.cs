using Hung.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hung.Weapons.Components
{
    public class MovementAttack : WeaponComponent<MovementData, AttackMovement>
    {

        private Movement coreMovement;
        private Movement CoreMovement => coreMovement ? coreMovement : Core.GetCoreComponent(ref coreMovement);
        private void HandlerStartAttackMovement()
        {
            //var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }
        private void HandlerStopAttackMovement()
        {
            CoreMovement.SetVelocityZero();
        }
        protected override void Start()
        {
            base.Start();

            eventHandler.OnStartAttackMovement += HandlerStartAttackMovement;
            eventHandler.OnStopAttackMovement += HandlerStopAttackMovement;
        }
        protected override void OnDesTroy()
        {
            base.OnDesTroy();

            eventHandler.OnStartAttackMovement -= HandlerStartAttackMovement;
            eventHandler.OnStopAttackMovement -= HandlerStopAttackMovement;
        }
    }
}

