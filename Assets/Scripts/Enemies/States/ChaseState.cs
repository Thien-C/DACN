using UnityEngine;

public class ChaseState : State
{
    protected D_ChargeState stateData;
    protected bool isChaseOverTime;
    protected Transform playerTransform;
    protected bool isPlayerInCloseRange;

    public ChaseState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData)
        : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        FindPlayer();
        isPlayerInCloseRange = entity.CheckPlayerInFlyMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isChaseOverTime = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (playerTransform != null)
        {
            Vector2 enemyPosition = entity.transform.position;
            Vector2 playerPosition = playerTransform.position;
            float distanceX = Mathf.Abs(playerPosition.x - enemyPosition.x);
            float distanceY = Mathf.Abs(playerPosition.y - enemyPosition.y);
            if (distanceX > 0.1f)
            {
                Vector2 moveDirection;

                if (distanceY >= 3)
                {
                    moveDirection = (playerPosition - enemyPosition).normalized;
                }
                else
                {
                    moveDirection = new Vector2(Mathf.Sign(playerPosition.x - enemyPosition.x), 0);
                }

                entity.transform.position = Vector2.MoveTowards(enemyPosition,
                    enemyPosition + moveDirection * stateData.chargeSpeed * Time.deltaTime,
                    stateData.chargeSpeed * Time.deltaTime);
            }
            if (Time.time >= startTime + stateData.chargeTime)
            {
                isChaseOverTime = true;
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void FindPlayer()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerTransform = player ? player.transform : null;
        }
    }
}
