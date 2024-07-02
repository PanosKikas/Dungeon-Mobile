using UnityEngine;

public class ProjectileTriggerCondition : Condition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
        {
            Complete();
        }
    }
}
