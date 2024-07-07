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
        [SerializeField] private CharacterBattleAnimator animator;
        [SerializeField] private GameObject highlight;
        
        public void Set(BattleCharacter character)
        {
            healthBar.Set(character.Health, character.Stats.MaxHealthStat);
            enduranceBar.Set(character.Endurance, character.Stats.MaxEnduranceStat);
            // TODO: Consider making this reactive in case it can change during battle.
            levelText.text = $"Level: {character.Level}";
            animator.Initialize(character.CharacterId);
        }

        public void Highlight()
        {
            highlight.SetActive(true);
        }

        public void Unhighlight()
        {
            highlight.SetActive(false);
        }
    }
}