using System;
using DMT.Character.Stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DMT.Character
{
    public class Character : MonoBehaviour
    {
        public CharacterStats stats { get; private set; }

        [SerializeField] private CharacterStatsSO initialStats;

        private FSM _stateMachine;
        
        private void Awake()
        {
            stats = new(initialStats);
        }

        private void Update()
        {
            _stateMachine.LogicUpdate();
        }
        
        private void FixedUpdate()
        {
            _stateMachine.PhysicsUpdate();
        }

        public bool IsAlive => stats.CurrentHealth > 0;
        
        public void TakeDamage(float damage)
        {
            stats.CurrentHealth = Mathf.Max((int)(stats.CurrentHealth - damage), 0);

            //Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
            // GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
            //  Destroy(impact, 2f);

            if (!IsAlive)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"Character {gameObject.name} has died ");
            //this.enabled = false;
            // gameObject.SetActive(false);
        }
    }
}