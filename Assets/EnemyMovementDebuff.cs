using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyMovementDebuff : MonoBehaviour, IMovementDebuffs
{
    [SerializeField, Range(0f, 1f)]
    float slowDownRate = .4f;

    public void DebuffMovement()
    {
        StartCoroutine(Stagger());
    }

    public IEnumerator Stagger()
    {
        AIPath path = GetComponent<AIPath>();
        float speed = path.maxSpeed;
        path.maxSpeed *= slowDownRate;
        yield return new WaitForSeconds(.1f);
        path.maxSpeed = speed;
    }


}
