using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolState : State
{ 
    [SerializeField]
    float searchRadius = 5f;

    Rigidbody2D rb;

    private Transform _source;
    
    Transform[] _patrolWaypoints;

    int currentWaypointIndex;
    Transform currentWaypoint;
    Vector3 targetWaypointPosition;

    [SerializeField]
    float speed = 5f;

    Vector2 velocity;

    Vector3 previousPosition;

    private SpriteRenderer _spriteRenderer;

    EnemyBehavior enemyBehavior;

    private float _waitingCountdown = 0f;

    public PatrolState(Transform source, SpriteRenderer spriteRenderer,
        IEnumerable<Transform> wayPoints)
    {
        _source = source;
        rb = source.GetComponent<Rigidbody2D>();
        _spriteRenderer = spriteRenderer;
        enemyBehavior = source.GetComponent<EnemyBehavior>();
        _patrolWaypoints = wayPoints.ToArray();
        _waitingCountdown = 0f;
    }

    public override void EnterState()
    {
        currentWaypointIndex = Random.Range(0, _patrolWaypoints.Length);
        currentWaypoint = _patrolWaypoints[currentWaypointIndex];
        targetWaypointPosition = currentWaypoint.position + Random.onUnitSphere * .4f;
    }
    
    public override void LogicUpdate(float delta)
    {
        if (_waitingCountdown > 0f)
        {
            _waitingCountdown -= delta;
            return;
        }
        
        if (CloseToWaypoint())
        {
            ArriveAtWaypoint();
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        var direction = GetMoveDirection();
        var currentPosition = _source.position;
        rb.MovePosition(currentPosition + direction * speed * delta);
        UpdateVelocity(delta);

        previousPosition = currentPosition;
        enemyBehavior.Velocity = velocity;
    }

    Vector3 GetMoveDirection()
    {
        return (targetWaypointPosition - _source.position).normalized;
    }

    bool CloseToWaypoint()
    {
        return Vector2.Distance(_source.position, targetWaypointPosition) <= .1f;
    }

    void ArriveAtWaypoint()
    {
        _source.position = targetWaypointPosition;
        velocity = Vector3.zero;
        _waitingCountdown = 3f;
    }
    
    void UpdateVelocity(float delta)
    {
        velocity = (_source.transform.position - previousPosition) / delta;
        velocity *= speed;
    }

    public override void ExitState()
    {
        enemyBehavior.Velocity = Vector2.zero;
    }
    
}
