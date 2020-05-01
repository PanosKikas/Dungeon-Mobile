using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
