using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using DMT.Characters;
using DMT.Characters.Stats;

namespace DMT.UI.Screen
{
    public class CharacterPreviewUI : MonoBehaviour
    {
        [SerializeField]
        private HPBarUI hpBar;

        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private CharacterStatsUI characterStatsUI;

        private CharacterStats characterStats;

        public void SetTo(Character character)
        {
            characterStats = character.stats;
            hpBar.SetToStats(characterStats);
            levelText.text = string.Format($"Level {character.Level}");
            
        }

        public void OpenCharacterStatsPanel()
        {
            characterStatsUI.ShowPanel();
            characterStatsUI.SubscribeTo(characterStats);
        }
    }
}