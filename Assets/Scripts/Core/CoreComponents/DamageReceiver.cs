using Hung.Combat.Damage;
using Hung.ModifierSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hung.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticle;
        [SerializeField] private GameObject textDamage;
        private Stats stats;
        private ParticleManager particleManager;
        private Animator anim;
        public Modifiers<Modifier<DamageData>, DamageData> Modifiers { get; } = new();
        public void Damage(DamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            if (data.Amount <= 0f)
            {
                return;
            }

            stats.Health.Decrease(data.Amount);
            particleManager.StartParticleWithRandomRotation(damageParticle);
            if(core.transform.parent.tag == "Player")
            {
                SoundManager.Instance.PlaySound(GameEnum.ESound.gotHit);
            }
            if (textDamage)
            {
                ShowFloatingText(data.Amount);
            }
            //anim.SetTrigger("damage");
        }


        void ShowFloatingText(float amount)
        {
            GameObject go = Instantiate(textDamage, transform.position, Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = "-" + amount.ToString();
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
            particleManager = core.GetCoreComponent<ParticleManager>();
            anim = GetComponentInParent<Animator>();
        }


    }
}

