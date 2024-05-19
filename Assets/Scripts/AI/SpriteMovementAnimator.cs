using UnityEngine;

public class SpriteMovementAnimator : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void AnimateMovement(Vector2 velocity)
    {
        animator.SetFloat("Speed", velocity.magnitude);
        if (velocity.x > 0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else if (velocity.x < 0.1f && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }
}
