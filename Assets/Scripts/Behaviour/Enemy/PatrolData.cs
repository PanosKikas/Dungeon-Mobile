using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PatrolData", menuName ="Behaviour/Enemy/Patrol")]
public class PatrolData : ScriptableObject
{
    public float Speed = 5f;
    public float Radius = 5f;
    public float ArriveWaitTime = 1.5f;
}
