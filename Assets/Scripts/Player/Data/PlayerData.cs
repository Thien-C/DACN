using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJump = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public float wallJumpCoyoteTime = 0.1f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffSet;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashCoolDown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndMultiplier = 0.2f;
    public float disBetweenAfterImages = 0.5f;

    [Header("Crouch State")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 0.8f;
    public float standColliderHeight = 1.6f;
    [Header("Projecttile")]
    public GameObject projectile;
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance;
}
