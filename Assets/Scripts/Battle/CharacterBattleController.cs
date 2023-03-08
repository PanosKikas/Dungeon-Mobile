using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterBattleController : MonoBehaviour
{
    public Character Owner;

    private BattleFSM stateMachine;

    public CharacterBattleController(Character owner)
    {
        stateMachine = new BattleFSM(owner);
    }

    protected virtual void Update()
    {
        stateMachine.LogicUpdateCurrentState();
    }

    public void Select()
    {
        stateMachine.ChangeState(stateMachine.ManualAttackState);
    }

    public void Deselect()
    {
        stateMachine.ChangeState(stateMachine.AutoAttackState);
    }
}
