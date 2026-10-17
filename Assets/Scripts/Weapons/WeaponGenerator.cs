using Hung.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hung.Weapons
{
    //Tu dong them cac component cho vu khi
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponDataSO data;

        private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentDependencies = new List<Type>();

        private Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();

            GenerateWeapon(data);
        }

        //test 
        [ContextMenu("Test Generate")]
        private void TestGenerator()
        {
            GenerateWeapon(data);
        }

        //Ham khoi tao ra cac scrip can co cua vu khi
        public void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            if (data is null)
            {
                weapon.SetCanEnterAttack(false);
                return;
            }

            componentAlreadyOnWeapon.Clear();
            componentAddedToWeapon.Clear();
            componentDependencies.Clear();
            componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

            componentDependencies = data.GetAllDependencies();

            foreach (var dependency in componentDependencies)
            {
                if (componentAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                {
                    // Nếu đã có component kiểu dependency, thực hiện hành động
                    continue;
                }
                var weaponComponent = componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }

                weaponComponent.Init();

                componentAddedToWeapon.Add(weaponComponent);
            }
            var componentsToRemove = componentAlreadyOnWeapon.Except(componentAddedToWeapon);

            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
            anim.runtimeAnimatorController = data.AnimationController;

            weapon.SetCanEnterAttack(true);
        }
    }

}
