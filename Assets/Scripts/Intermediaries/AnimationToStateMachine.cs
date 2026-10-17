using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    // tạo để thay thế event trong animation
    public AttackState  attackState;
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }
    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
