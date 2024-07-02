using DMT.Characters;
using DMT.Characters.Inventory;
using DMT.Characters.Stats;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    private Rigidbody2D rb;

    private FSM stateMachine;
    public Vector2 Velocity { get; set; }

    public State patrolState { get; private set; }
    public State chaseState { get; private set; }

    private EnemyGroup enemyGroup;
    private SpriteMovementAnimator animator;

    [Header("Patrol Data")]
    [SerializeField]
    private Waypoints waypoints;
    [SerializeField]
    private PatrolData patrtolData;

    [SerializeField]
    private InitialCharacterData data;

    private Character character;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyGroup = GetComponentInParent<EnemyGroup>();
        animator = GetComponentInChildren<SpriteMovementAnimator>();
        stateMachine = new FSM();
        character = new Character(data, new NullInventory());
        patrolState = new PatrolState(this, stateMachine, waypoints.PatrolWaypoints, patrtolData);
        chaseState = new ChaseState(this, enemyGroup, stateMachine);
    }

    private void Start()
    {
        stateMachine.ChangeState(patrolState);
    }

    private void Update()
    {
        stateMachine.LogicUpdate(Time.deltaTime);
        animator.AnimateMovement(Velocity);
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate(Time.fixedDeltaTime);
        Move();
    }

    private void Move()
    {
        rb.velocity = Velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Activating combat mode");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        stateMachine.OnTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stateMachine.OnTriggerExit(collision);
    }

    public void TakeDamage(int damage)
    {
        character.TakeDamage(damage);
    }
}
