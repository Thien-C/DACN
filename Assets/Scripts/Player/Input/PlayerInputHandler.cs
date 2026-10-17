using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private bool inputActive = true;
    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    //test fly
    public bool FlyInput { get; private set; }
    public bool FlyInputStop { get; private set; }  
    //test
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool[] AttackInputs { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        //lay ra gia tri trong enum
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if (!inputActive) return;
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }
        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.primary] = false;
        }
    }
    //public void OnSecondaryAttack(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        AttackInputs[(int)CombatInputs.secondary] = true;
    //    }
    //    if (context.canceled)
    //    {
    //        AttackInputs[(int)CombatInputs.secondary] = false;
    //    }
    //}
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!inputActive) return;
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
      
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!inputActive) return;
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (!inputActive) return;
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (!inputActive) return;
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }

    }
    public void OnFlyInput(InputAction.CallbackContext context)
    {
        if (!inputActive) return;
        if (context.started) // Khi bắt đầu nhấn phím
        {
            FlyInput = true;
            FlyInputStop = false;
        }
        if (context.canceled) // Khi thả phím
        {
            FlyInputStop = true;
            FlyInput = false;
        }
    }


    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        if (Camera.main == null || !inputActive) return;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Đảm bảo không thay đổi trục Z
        RawDashDirectionInput = (mouseWorldPos - transform.position).normalized;
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput);
    }

    public void SetInputActive(bool active)
    {
        inputActive = active;
        if (!inputActive)
        {
            ResetInputValues();
        }
    }

    private void ResetInputValues()
    {
        RawMovementInput = Vector2.zero;
        NormInputX = 0;
        NormInputY = 0;
        JumpInput = false;
        JumpInputStop = true;
        DashInput = false;
        DashInputStop = true;
        GrabInput = false;
        FlyInput = false;
        FlyInputStop = true;
        if (AttackInputs != null)
        {
            for (int i = 0; i < AttackInputs.Length; i++)
            {
                AttackInputs[i] = false;
            }
        }
    }


    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    public void UseAttackInput(int i) => AttackInputs[i] = false;
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}


public enum CombatInputs
{
    primary,
    secondary,
}