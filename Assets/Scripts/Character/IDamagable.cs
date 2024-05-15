using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.Characters
{
    public interface IDamagable
    {
        void TakeDamage(int damage);
    }
}