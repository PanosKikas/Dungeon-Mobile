using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.Characters
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}