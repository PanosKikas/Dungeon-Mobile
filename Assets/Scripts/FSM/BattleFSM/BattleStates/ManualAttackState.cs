using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ManualAttackState : AttackState
{

    PlayerBattle playerBattle;
    CinemachineImpulseSource impulseSource;
    PlayerCharacterStats playerStats;

    public ManualAttackState(FSM stateMachine) : base(stateMachine)
    {
        impulseSource = stateMachine.GetComponent<CinemachineImpulseSource>();
        playerBattle = (PlayerBattle)battle;
        
    }

    public override void EnterState()
    {
        base.EnterState();
        playerStats = (PlayerCharacterStats)playerBattle.playerStats;
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
                nextFire = Time.time + 1f / playerStats.ManualAttackRate;
                ShakeCamera();
                PlayerStatusEffects.DecreaseEndurance(playerStats);
            }
        }
    }

    protected override void FindTarget()
    {
        playerBattle.FindManualAttackTarget();
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && playerBattle.playerStats.HasEndurance();
    }

    void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
