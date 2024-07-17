using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float speed = 10f;
    [SerializeField] private int projectileDamage;
    [SerializeField] private float projectileLifetime = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartMovingUp();
        Destroy(gameObject, projectileLifetime);
    }

    private void StartMovingUp()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage(projectileDamage);
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