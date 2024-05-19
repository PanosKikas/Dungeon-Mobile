using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private float speed = 10f;

    public int ProjectileDamage { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartMovingUp();
        DestroyAfter(10);
    }

    private void StartMovingUp()
    {
        rb.velocity = transform.up * speed;
    }

    void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision is IDamagable damagable)
        {
            damagable.TakeDamage(ProjectileDamage);
        }
        Explode();
    }

    void Explode()
    {
        Idle();
        Animate();
    }

    void Idle()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.localScale = Vector3.one;
        rb.velocity = Vector2.zero;
    }

    void Animate()
    {
        animator.SetTrigger("Explode");
    }

    // called when explosion animation is finished
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
