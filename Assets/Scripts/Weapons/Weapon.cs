using Hung.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Video.VideoPlayer;
namespace Hung.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action<bool> OnCurrentInputChange;

        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnUseInput;

        [SerializeField] private float attackCounterResetCooldown;
        public bool CanEnterAttack { get; private set; }
        public WeaponDataSO Data { get; private set; }
        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set
            {
                if (value >= Data.NumberOfAttacks)
                {
                    currentAttackCounter = 0;
                }
                else
                {
                    currentAttackCounter = value;
                }
            }
        }

        public bool CurrentInput
        {
            get => currentIput;
            set
            {
                if (currentIput != value)
                {
                    currentIput = value;
                    OnCurrentInputChange?.Invoke(currentIput);
                }
            }
        }
        public float AttackStartTime { get; private set; }

        private Animator anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        private AnimationEventHandler eventHandler;
        public AnimationEventHandler EventHandler
        {
            get
            {
                if (!initDone)
                {
                    GetDependencies();
                }

                return eventHandler;
            }
            private set => eventHandler = value;
        }
        public Core Core { get; private set; }

        private int currentAttackCounter;

        private TimeNotifier attackCounterResetTimeNotifier;

        private bool currentIput;
        private bool initDone;

        public void Enter()
        {
            print($"{transform.name} enter");

            AttackStartTime = Time.time;

            attackCounterResetTimeNotifier.Disable();

            anim.SetBool("active", true);
            anim.SetInteger("counter", CurrentAttackCounter);

            OnEnter?.Invoke();
        }

        public void SetCore(Core core)
        {
            Core = core;
        }

        public void SetData(WeaponDataSO data)
        {
            Data = data;
        }
        public void SetCanEnterAttack(bool value) => CanEnterAttack = value;
        public void Exit()
        {
            anim.SetBool("active", false);

            CurrentAttackCounter++;

            attackCounterResetTimeNotifier.Init(attackCounterResetCooldown);

            OnExit?.Invoke();
        }

        private void GetDependencies()
        {
            if (initDone)
                return;

            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            anim = BaseGameObject.GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            initDone = true;
        }

        private void Update()
        {
            attackCounterResetTimeNotifier.Tick();
        }

        private void Awake()
        {
            GetDependencies();

            attackCounterResetTimeNotifier = new TimeNotifier();
        }
        private void ResetAttackCounter()
        {
            print("Reset Attack Counter");
            CurrentAttackCounter = 0;
        } 

        private void OnEnable()
        {
            EventHandler.OnUseInput += HandleUseInput;
            attackCounterResetTimeNotifier.OnNotify += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnUseInput -= HandleUseInput;
            attackCounterResetTimeNotifier.OnNotify -= ResetAttackCounter;
        }

        private void HandleUseInput() => OnUseInput?.Invoke();
    }

}
