using System;
using System.Collections.Generic;
using DMT.Characters;
using DMT.Characters.Inventory;
using DMT.Characters.Stats;
using DMT.Controllers;
using DMT.Persistent;
using NUnit.Framework;
using UnityEngine;
using UniRx;

public class EnemyController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;

    private FSM stateMachine;
    public Vector2 Velocity { get; set; }

    public State patrolState { get; private set; }
    public State chaseState { get; private set; }

    private SpriteMovementAnimator animator;

    [Header("Patrol Data")] [SerializeField]
    private Waypoints waypoints;

    [SerializeField] private PatrolData patrtolData;

    [SerializeField] private InitialCharacterData data;

    public Character Character { get; private set; }
    public EnemyGroup EnemyGroup { get; set; }

    private readonly List<IDisposable> characterSubscriptions = new();
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<SpriteMovementAnimator>();
        Character = new Character(data, new NullInventory());
        stateMachine = new FSM();
        Character.CharacterDied.Subscribe(_ => Destroy(gameObject)).AddTo(characterSubscriptions);
    }

    private void Start()
    {
        patrolState = new PatrolState(this, stateMachine, waypoints.PatrolWaypoints, patrtolData);
        chaseState = new ChaseState(this, EnemyGroup, stateMachine);
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
            var player = collision.gameObject.GetComponent<Player>();
            Assert.IsNotNull("Player interacted with enemy should never be null.");
            EnemyGroup.EngageInCombat(player);
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
        Character.TakeDamage(damage);
    }

    public void ChaseTarget()
    {
        if (stateMachine.currentState != chaseState)
        {
            stateMachine.ChangeState(chaseState);
        }
    }

    private void OnDestroy()
    {
        characterSubscriptions.DisposeAndClear();
    }
}