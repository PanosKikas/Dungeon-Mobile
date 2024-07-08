using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace DMT.Battle
{
    public class BattleCharacterAnimator : MonoBehaviour
    {
        public readonly Subject<Unit> OnDamage = new();
        [SerializeField] private Animator animator;

        private static readonly int BaseAttackTrigger = Animator.StringToHash("Attack");

        private readonly List<IDisposable> subscriptions = new();
        
        public void Initialize(string characterId)
        {
            animator.runtimeAnimatorController = CharacterUtils.GetCharacterAnimatorController(characterId);
        }
        
        public void Attack(Action callback)
        {
            subscriptions.DisposeAndClear();
            OnDamage.Subscribe(_ => callback?.Invoke()).AddTo(subscriptions);
            animator.SetTrigger(BaseAttackTrigger);
        }
        
        public void AnimationHit()
        {
            OnDamage.OnNext(new());
            subscriptions.DisposeAndClear();
        }
    }
}