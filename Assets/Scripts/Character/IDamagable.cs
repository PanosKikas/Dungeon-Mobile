using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.Character
{
    public interface IDamagable
    {
        void TakeDamage(int damage);
    }
}