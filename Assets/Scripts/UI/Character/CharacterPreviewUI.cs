using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using DMT.Characters;
using DMT.Characters.Stats;
using UniRx;
using UnityEngine.Serialization;

namespace DMT.Characters.UI
{
    public class CharacterPreviewUI : MonoBehaviour
    {
        [SerializeField]
        private StatBarUI statBar;

        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private CharacterStatsUI characterStatsUI;

        [SerializeField] private TextMeshProUGUI nameText;
        
        private CharacterStats characterStats;

        public void SetTo(Character character)
        {
            characterStats = character.Stats;
            statBar.Set(character.CurrentHealth, characterStats.MaxHealthStat);
            levelText.text = string.Format($"Level {character.Level}");
            nameText.text = character.CharacterName;
        }

        public void OpenCharacterStatsPanel()
        {
            characterStatsUI.ShowPanel();
        }
    }
}