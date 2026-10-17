using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

public class Entity : MonoBehaviour
{
    public Movement Movement { get => movement ?? Core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? Core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    //private Stats Stats { get => stats ?? Core.GetCoreComponent<Stats>(ref stats); }
    //private Stats stats;

    //private HealthBarBehavior healthBar;

    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }

    [SerializeField] private Transform playerCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStuned;
    protected bool isDead;

    protected Stats stats;
    public virtual void Awake()
    {
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;


        Core = GetComponentInChildren<Core>();

        stats = Core.GetCoreComponent<Stats>();

        anim = GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
        atsm = GetComponent<AnimationToStateMachine>();

    }
    public virtual void Update()
    {
        Core.LogicUpdate();

        stateMachine.curentState.LogicUpdate();

        anim.SetFloat("yVelocity", Movement.RB.linearVelocity.y);

        if (Time.time >= lastDamageTime + entityData.stunRecoverTime)
        {
            ResetStunResistance();
        }
    }
    public virtual void FixUpdate()
    {
        stateMachine.curentState.PhysicsUpdate();
    }


    //Check quai tren ground
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }


    //Check quai bay
    public virtual bool CheckPlayerInFlyMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.down, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInFlyMaxAgroRange()
    {
        return Physics2D.OverlapCircle(playerCheck.position, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.linearVelocity.x, velocity);
        Movement.RB.linearVelocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStuned = false;
        currentStunResistance = entityData.stunResistance;
    }
    
    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            //Gizmos.DrawLine(CollisionSenses.WallCheck.position, CollisionSenses.WallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * CollisionSenses.WallCheckDistance));
            //Gizmos.DrawLine(CollisionSenses.LedgeCheckVertical.position, CollisionSenses.LedgeCheckVertical.position + (Vector3)(Vector2.down * CollisionSenses.LedgeCheckVerticalDistance));
            //Gizmos.DrawWireSphere(CollisionSenses.GroundCheck.position, CollisionSenses.GroundCheckRadius);
            //Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            //Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            //Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        }
    }
}
