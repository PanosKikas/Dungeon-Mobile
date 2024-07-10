using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using DMT.Characters.Stats;
using UnityEngine;
using Random = UnityEngine.Random;
using UniRx;
using UnityEngine.Serialization;

namespace DMT.Battle
{
    public class BattleCharacter : MonoBehaviour, IDamagable
    {
        public bool CanTick { get; set; } 
        [FormerlySerializedAs("characterAnimator")] [SerializeField] private BattleCharacterAnimator battleCharacterAnimator;

        private Character character;
        private BattleCharacter lastTarget;

        private float nextActionTime = 0f;
        private ITargetSelector targetSelector;
        private IEnumerable<BattleCharacter> enemyCharacters => enemyTeam.GetCharacters();

        private IEnumerable<BattleCharacter> aliveEnemies => enemyCharacters.Where(e => e.IsAlive());
        
        private bool isPossessed;

        private BattleTeam characterTeam;
        private BattleTeam enemyTeam;
        private bool isAttacking = false;

        public IObservable<float> Health => character.CurrentHealth;
        public IObservable<float> Endurance => character.CurrentEndurance;
        public Subject<BattleCharacter> CharacterDied = new();
        public CharacterStats Stats => character.Stats;
        public int Level => character.Level.Value;
        public string CharacterId => character.Id;

        private readonly List<IDisposable> subscriptions = new();
        
        public void Initialize(Character character, BattleTeam team, BattleTeam enemyTeam)
        {
            subscriptions.DisposeAndClear();
            this.character = character;
            characterTeam = team;
            this.enemyTeam = enemyTeam;
            targetSelector = new RandomTargetSelector();
            nextActionTime = Random.Range(0f, 0.5f);
            character.CharacterDied.Subscribe(_ => OnCharacterDied()).AddTo(subscriptions);
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
            nextActionTime -= Time.deltaTime;
            
            if (isPossessed || isAttacking)
            {
                return;
            }
            
            if (nextActionTime <= 0)
            {
                Act();
            }
        }
        
        private void Act()
        {
            var target = targetSelector.SelectFrom(aliveEnemies);
            Attack(target);
        }

        public void ManualAttack(BattleCharacter target)
        {
            if (isAttacking)
            {
                return;
            }
            
            if (character.CurrentEndurance.Value < character.Stats.EndurancePerAttack)
            {
                return;
            }
            
            character.CurrentEndurance.Value -= character.Stats.EndurancePerAttack;
            Attack(target);
        }

        private void Attack(BattleCharacter target)
        {
            isAttacking = true;
            var damage = (int)character.Stats.AttackDamageStat.Value;
            battleCharacterAnimator.Attack(() => DamageTarget(target, damage));
            nextActionTime += 1f/character.Stats.AutoAttackRateStat.Value;
        }

        private void DamageTarget(BattleCharacter target, int damage)
        {
            isAttacking = false;
            
            if (!target.IsAlive())
            {
                return;
            }

            target.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            character.TakeDamage(damage);
        }

        private void OnCharacterDied()
        {
            CanTick = false;
            CharacterDied.OnNext(this);
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