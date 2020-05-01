using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;

    [SerializeField]
    float speed = 5f;

    Vector2 input = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        GetInput();
        Animate();       
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 deltaVelocity = input * speed * Time.fixedDeltaTime;
        rb.velocity = deltaVelocity;
    }

    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        NormalizeInput();
    }

    void NormalizeInput()
    {
        input = Vector2.ClampMagnitude(input, 1f);
    }

    private void Animate()
    {
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
    }
    
    
}
