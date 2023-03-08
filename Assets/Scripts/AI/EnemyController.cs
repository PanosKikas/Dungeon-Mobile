using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    EnemyFSM stateMachine;
    public Vector2 Velocity { get; set; }
    MovementAnimation animateMovement;

    [SerializeField] private CharacterStatsSO characterStats;
    public Character Character { get; private set; }

    private void Awake()
    {
        Character = new Character(characterStats);
        stateMachine = new EnemyFSM(this);
        animateMovement = GetComponent<MovementAnimation>();
    }

    private void Update()
    {
        stateMachine.LogicUpdateCurrentState();
        animateMovement.AnimateMovement(Velocity);
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdateCurrentState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterBattle();
          //  Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stateMachine.currentState != stateMachine.ChaseState)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Projectile"))
            {
                stateMachine.ChangeState(stateMachine.ChaseState);
            }
                   
        }
    }

    void EnterBattle()
    {
        BattleTransistor.Instance.EnterBattleScene(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (stateMachine.currentState == stateMachine.ChaseState && collision.CompareTag("Player"))
        {
            stateMachine.ChangeState(stateMachine.PatrolState);
        }
            
    }

    public void StartChase()
    {
        if (stateMachine.currentState != stateMachine.ChaseState)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }
    }

}
