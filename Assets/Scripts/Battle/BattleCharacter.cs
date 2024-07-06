using System;
using System.Collections.Generic;
using DMT.Characters;
using DMT.Characters.Stats;
using UnityEngine;

namespace DMT.Battle
{
    public class BattleCharacter : MonoBehaviour, IDamagable
    {
        public bool CanTick { get; set; } = false;
        [SerializeField] private Animator characterAnimator;

        private Character character;
        private BattleCharacter lastTarget;
        private static readonly int Attack1 = Animator.StringToHash("Attack");

        private float nextActionTime;
        private ITargetSelector targetSelector;
        private IEnumerable<BattleCharacter> enemyCharacters => enemyTeam.GetCharacters();

        private bool isPossessed;

        private BattleTeam characterTeam;
        private BattleTeam enemyTeam;

        public CharacterStats Stats => character.Stats;
        public int Level => character.Level.Value;
        public string CharacterId => character.Id;
        
        public void Initialize(Character character, BattleTeam team, BattleTeam enemyTeam)
        {
            this.character = character;
            this.characterTeam = team;
            this.enemyTeam = enemyTeam;
        }
        
        private void Update()
        {
            if (!CanTick)
            {
                return;
            }
            
            RechargeEndurance();
            
            if (isPossessed)
            {
                return;
            }
            
            nextActionTime -= Time.deltaTime;
            if (nextActionTime <= 0)
            {
                Act();
            }
        }
        
        private void Act()
        {
            Attack();
        }

        public void Attack()
        {
            if (targetSelector == null)
            {
                return;
            }
            
            var target = targetSelector.Select(enemyCharacters);
            nextActionTime += character.Stats.AutoAttackRateStat.Value;
            character.Stats.CurrentEndurance.Value -= 10;
            characterAnimator.SetTrigger(Attack1);
            target.TakeDamage((int)character.Stats.AttackDamageStat.Value);
        }

        public void TakeDamage(int damage)
        {
            character.TakeDamage(damage);
        }

        public void Possess()
        {
            isPossessed = true;
        }

        public void Unpossess()
        {
            isPossessed = false;
        }

        private void RechargeEndurance()
        {
            character.Stats.CurrentEndurance.Value += character.Stats.EnduranceRegenStat.Value * Time.deltaTime;
        }
    }
}