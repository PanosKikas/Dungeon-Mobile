using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleFSM : BattleFSM
{ 
    public ManualAttackState ManualAttackState { get; private set; }

    protected override void Start()
    {
        ManualAttackState = new ManualAttackState(this);
        base.Start();
    }
    
}
