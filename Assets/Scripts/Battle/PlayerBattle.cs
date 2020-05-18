using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;


public class PlayerBattle : CharacterBattle<PlayerCharacterStats>
{   
    [SerializeField]
    LayerMask enemyLayerMask;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        PlayerStatusEffects.RechargeEndurance(stats);
    }

    public void FindManualAttackTarget()
    {
        FindClosestTargetToMousePosition();    
    }

    void FindClosestTargetToMousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, 2f, enemyLayerMask);
        if (colliders != null && colliders.Any<Collider2D>())
        {
            Target = colliders[0].gameObject.GetComponent<CharacterBattle<EnemyCharacterStats>>().stats;
        }
        else
        {
            Target = null;
        }
    } 
}
