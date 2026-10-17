using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using Hung.CoreSystem;
using Hung.Weapons;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class Player : MonoBehaviour
{
    #region State Variable
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private Transform bow;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    private Movement Movement { get => movement ?? Core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? Core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    #endregion

    #region Other Variable
    private Vector2 workSpace;
    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;

    private float baseSpeed;    // Tốc độ cơ bản từ playerData
    private float currentSpeed; // Tốc độ hiện tại (có thể thay đổi bởi item)
    #endregion

    #region Unity CallBack Function
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Core = GetComponentInChildren<Core>();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", primaryWeapon, CombatInputs.primary);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", secondaryWeapon, CombatInputs.secondary);
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();
        StateMachine.Initialize(IdleState);

        baseSpeed = playerData.movementVelocity; // Lưu tốc độ cơ bản
        currentSpeed = baseSpeed;                // Khởi tạo tốc độ hiện tại
    }
    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions

    //Set collider 
    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workSpace.Set(MovementCollider.size.x, height);
        center.y += (height - MovementCollider.size.y) / 2;
        MovementCollider.size = workSpace;
        MovementCollider.offset = center;
    }

    //Va cham voi trap
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("RockHead") || (other.gameObject.CompareTag("Enemy"))) // Kiểm tra nếu Player chạm vào hitbox
        {
            IDamageable damageable = Core.GetComponentInChildren<IDamageable>();
            damageable.Damage(new DamageData(10, Core.Root));
            IKnockBackable kn = Core.GetComponentInChildren<IKnockBackable>();
            kn.KnockBack(new KnockBackData(new Vector2(1, 1), 20, Movement.FacingDirection, Core.Root));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) // Kiểm tra nếu Player chạm vào hitbox
        {
            IDamageable damageable = Core.GetComponentInChildren<IDamageable>();
            damageable.Damage(new DamageData(10, Core.Root));
            IKnockBackable kn = Core.GetComponentInChildren<IKnockBackable>();
            kn.KnockBack(new KnockBackData(new Vector2(1, 1), 20, -Movement.FacingDirection, Core.Root));
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap")) // Kiểm tra nếu Player chạm vào hitbox
        {
            IDamageable damageable = Core.GetComponentInChildren<IDamageable>();
            damageable.Damage(new DamageData(2, Core.Root));
            IKnockBackable kn = Core.GetComponentInChildren<IKnockBackable>();
            kn.KnockBack(new KnockBackData(new Vector2(1, 1), 20, -Movement.FacingDirection, Core.Root));
        }
    }
    public void Respawn(Vector3 position)
    {
        transform.position = position;
        if (RB != null)
        {
            RB.linearVelocity = Vector2.zero;
        }
        gameObject.SetActive(true);
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.DrawWireSphere(CollisionSenses.GroundCheck.position, CollisionSenses.GroundCheckRadius);
            Gizmos.DrawWireSphere(CollisionSenses.CeilingCheck.position, CollisionSenses.GroundCheckRadius);
            Gizmos.DrawLine(CollisionSenses.WallCheck.position, new Vector3(CollisionSenses.WallCheck.position.x + CollisionSenses.WallCheckDistance, CollisionSenses.WallCheck.position.y, CollisionSenses.WallCheck.position.z));
            Gizmos.DrawLine(CollisionSenses.LedgeCheckHorizontal.position, new Vector3(CollisionSenses.LedgeCheckHorizontal.position.x + CollisionSenses.WallCheckDistance, CollisionSenses.LedgeCheckHorizontal.position.y, CollisionSenses.LedgeCheckHorizontal.position.z));
        }
    }
    #endregion

    #region tang hinh player
    public void ActivateInvisibility(SpriteRenderer renderer, float duration)
    {
        StartCoroutine(ApplyInvisibility(renderer, duration));
    }

    private IEnumerator ApplyInvisibility(SpriteRenderer playerRenderer, float invisibilityDuration)
    {
        Color originalColor = playerRenderer.color;
        Color invisibleColor = originalColor;
        invisibleColor.a = 0.2f; // Gần như trong suốt
        playerRenderer.color = invisibleColor;
        // Chờ thời gian tàng hình
        yield return new WaitForSeconds(invisibilityDuration);
        // Khôi phục màu ban đầu
        playerRenderer.color = originalColor;
    }
    #endregion


    #region Thay đổi tốc độ
    public void ActivateSpeedChange(float speedMultiplier, float duration, bool speedUp)
    {
        StartCoroutine(ApplySpeedChange(speedMultiplier, duration, speedUp));
    }

    private IEnumerator ApplySpeedChange(float speedMultiplier, float duration, bool speedUp)
    {
        // Tính toán tốc độ mới
        if (speedUp)
        {
            currentSpeed = baseSpeed * speedMultiplier; // Tăng tốc
        }
        else
        {
            currentSpeed = baseSpeed / speedMultiplier; // Giảm tốc
        }

        // Cập nhật tốc độ trong playerData
        playerData.movementVelocity = currentSpeed;

        // Chờ thời gian hiệu ứng
        yield return new WaitForSeconds(duration);

        // Khôi phục tốc độ ban đầu
        playerData.movementVelocity = baseSpeed;
        currentSpeed = baseSpeed;
    }
   
    #endregion
    
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
