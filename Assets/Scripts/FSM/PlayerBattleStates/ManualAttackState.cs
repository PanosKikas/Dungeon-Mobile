using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ManualAttackState : AttackState<PlayerCharacterStats, PlayerBattle>
{

  //  PlayerBattle playerBattle;
    CinemachineImpulseSource impulseSource;

    public ManualAttackState(FSM stateMachine) : base(stateMachine)
    {
        impulseSource = stateMachine.GetComponent<CinemachineImpulseSource>();
     //   playerBattle = (PlayerBattle)battle;
        
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
                nextFire = Time.time + 1f / stats.ManualAttackRate;
                ShakeCamera();
                PlayerStatusEffects.DecreaseEndurance(stats);
            }
        }
    }

    protected override void FindTarget()
    {
        battle.FindManualAttackTarget();
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && stats.HasEndurance();
    }

    void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
