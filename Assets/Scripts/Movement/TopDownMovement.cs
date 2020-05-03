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

#if UNITY_EDITOR_WIN

        if (UnityEditor.EditorApplication.isRemoteConnected)
        {
            GetInputAndroid();
        }
        else
        {
            GetInputWindows();
        }
#else

        GetInputAndroid();
#endif



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
            deltaVelocity.y *=  maxDeltaSpeed / deltaVelocity.magnitude;
        }
         
        rb.velocity = deltaVelocity;
    }

    private void GetInputWindows()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        NormalizeInput();
    }

    void GetInputAndroid()
    {
        input.x = joystick.Horizontal;
        input.y = joystick.Vertical;
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
