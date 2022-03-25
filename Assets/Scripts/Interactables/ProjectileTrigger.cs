using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : Condition

{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
        {
            Complete();
        }
    }

}
