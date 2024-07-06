using DMT.Characters;
using TMPro;
using UnityEngine;

namespace DMT.Battle.UI
{
    public class BattleCharacterHUD : MonoBehaviour
    {
        [SerializeField] private StatBarUI healthBar;
        [SerializeField] private StatBarUI enduranceBar;
        [SerializeField] private StatBarUI manaBar;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private CharacterPreviewAnimator animator;
        
        public void Set(BattleCharacter character)
        {
            healthBar.Set(character.Stats.CurrentHealth, character.Stats.MaxHealthStat);
            enduranceBar.Set(character.Stats.CurrentEndurance, character.Stats.MaxEnduranceStat);
            // TODO: Consider making this reactive in case it can change during battle.
            levelText.text = $"Level: {character.Level}";
            animator.ShowFor(character.CharacterId);
        }
    }
}