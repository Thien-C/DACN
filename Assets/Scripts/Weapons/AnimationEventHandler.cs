using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnStartAttackMovement;
    public event Action OnStopAttackMovement;
    public event Action OnAttackAction;
    public event Action OnMinHoldPassed;
    public event Action<AttackPhase> OnEnterAttackPhase;

    public event Action OnUseInput;
    public event Action<bool> OnFlipSetActive;
    public event Action OnEnableInterrupt;

    private void AnimationFinishTrigger() => OnFinish?.Invoke();
    private void StartAttackMovementTrigger() => OnStartAttackMovement?.Invoke();
    private void StopAttackMovementTrigger() => OnStopAttackMovement?.Invoke();
    private void AttackActionTrigger() => OnAttackAction?.Invoke();
    private void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();
    private void EnterAttackPhase(AttackPhase phase) => OnEnterAttackPhase?.Invoke(phase);

    private void SetFlipActive() => OnFlipSetActive?.Invoke(true);
    private void SetFlipInactive() => OnFlipSetActive?.Invoke(false);

    private void UseInputTrigger() => OnUseInput?.Invoke();
    private void EnableInterrupt() => OnEnableInterrupt?.Invoke();
}
