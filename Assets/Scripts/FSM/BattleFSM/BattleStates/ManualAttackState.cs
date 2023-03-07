using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ManualAttackState : AttackState
{

    PlayerBattle playerBattle;
    CinemachineImpulseSource impulseSource;
    Character _character;

    public ManualAttackState(BattleFSM stateMachine, Character character) : base(stateMachine)
    {
        impulseSource = stateMachine.GetComponent<CinemachineImpulseSource>();
        playerBattle = (PlayerBattle)battle;
        _character = character;
    }

    public override void EnterState()
    {
        base.EnterState();
        battle.Target = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetButtonDown("Fire1") && CanAttack())
        {
            FindTarget();
            if (battle.HasAttackTarget())
            {
                battle.AttackTarget();
                nextFire = Time.time + 1f / _character.Stats.AutoAttackRate.Value;
                ShakeCamera();
            }
        }
     /*   else if (Input.GetMouseButtonDown(2))
        {
            playerBattle.EnterParry();
        }*/
    }

    protected override void FindTarget()
    {
        playerBattle.FindManualAttackTarget();
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && playerBattle.playerStats.HasEnduranceForAttack();
    }

    void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
