using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ManualAttackState : AttackState
{

    PlayerBattle playerBattle;
    CinemachineImpulseSource impulseSource;
    PlayerCharacterStats playerStats;

    public ManualAttackState(BattleFSM stateMachine) : base(stateMachine)
    {
        impulseSource = stateMachine.GetComponent<CinemachineImpulseSource>();
        playerBattle = (PlayerBattle)battle;
        
    }

    public override void EnterState()
    {
        base.EnterState();
        playerStats = playerBattle.playerStats;
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
                nextFire = Time.time + 1f / playerStats.stats.ManualAttackRate;
                ShakeCamera();
                PlayerStatusEffects.DecreaseEndurance(playerStats);
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
