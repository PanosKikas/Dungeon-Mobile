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

    [SerializeField]
    Joystick joystick;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var KeyboardInput = GetKeyboardInput();
        var JoystickInput = GetJoystickInput();
        var UsedInput = (JoystickInput != Vector2.zero) ? JoystickInput : KeyboardInput;

        input = UsedInput;
        NormalizeInput();


            Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 deltaVelocity;


        deltaVelocity = input * speed * Time.fixedDeltaTime;


        if (input.magnitude >= .1f)
        {
            float maxDeltaSpeed = speed * Time.fixedDeltaTime;
            deltaVelocity.x *= maxDeltaSpeed / deltaVelocity.magnitude;
            deltaVelocity.y *= maxDeltaSpeed / deltaVelocity.magnitude;
        }

        rb.velocity = deltaVelocity;
    }

    private Vector2 GetKeyboardInput()
    {

        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }

    private Vector2 GetJoystickInput()
    {
        var x = joystick.Horizontal;
        var y = joystick.Vertical;
        return new Vector2(x, y);
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
