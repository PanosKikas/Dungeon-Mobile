using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using DMT.Characters.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DMT.Battle
{
    public class BattleCharacter : MonoBehaviour, IDamagable
    {
        public bool CanTick { get; set; } 
        [SerializeField] private CharacterBattleAnimator characterAnimator;

        private Character character;
        private BattleCharacter lastTarget;

        private float nextActionTime = 0f;
        private ITargetSelector targetSelector;
        private IEnumerable<BattleCharacter> enemyCharacters => enemyTeam.GetCharacters();

        private IEnumerable<BattleCharacter> aliveEnemies => enemyCharacters.Where(e => e.IsAlive());
        
        private bool isPossessed;

        private BattleTeam characterTeam;
        private BattleTeam enemyTeam;

        public IObservable<float> Health => character.CurrentHealth;
        public IObservable<float> Endurance => character.CurrentEndurance;
        public CharacterStats Stats => character.Stats;
        public int Level => character.Level.Value;
        public string CharacterId => character.Id;
        
        public void Initialize(Character character, BattleTeam team, BattleTeam enemyTeam)
        {
            this.character = character;
            this.characterTeam = team;
            this.enemyTeam = enemyTeam;
            this.targetSelector = new RandomTargetSelector();
            nextActionTime = Random.Range(0f, 1f/character.Stats.AutoAttackRateStat.Value);
        }
        
        private void Update()
        {
            if (!CanTick)
            {
                return;
            }
            
            if (!aliveEnemies.Any())
            {
                return;
            }
            
            RechargeEndurance();
            
            if (isPossessed)
            {
                return;
            }
            
            if (nextActionTime <= 0)
            {
                Act();
            }
            nextActionTime -= Time.deltaTime;
        }
        
        private void Act()
        {
            var target = targetSelector.SelectFrom(aliveEnemies);
            Attack(target);
        }

        public void ManualAttack(BattleCharacter target)
        {
            if (character.CurrentEndurance.Value < character.Stats.EndurancePerAttack)
            {
                return;
            }
            character.CurrentEndurance.Value -= character.Stats.EndurancePerAttack;
            Attack(target);
        }

        private void Attack(BattleCharacter target)
        {
            var damage = (int)character.Stats.AttackDamageStat.Value;
            characterAnimator.Attack(() => target.TakeDamage(damage));
            nextActionTime += 1f/character.Stats.AutoAttackRateStat.Value;
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

        public bool IsAlive()
        {
            return character.IsAlive;
        }

        private void RechargeEndurance()
        {
            character.CurrentEndurance.Value += character.Stats.EnduranceRegenStat.Value * Time.deltaTime;
        }
    }
}