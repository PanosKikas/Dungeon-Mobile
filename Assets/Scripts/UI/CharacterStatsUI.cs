using TMPro;
using UnityEngine;
using System.Text;
using DMT.Characters.Stats;

namespace DMT.UI.Screen
{
    public class CharacterStatsUI : MonoBehaviour
    {
        private CharacterStats characterStats;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private TextMeshProUGUI statsText;

        public void ShowFor(CharacterStats stats)
        {
            this.characterStats = stats;
            canvasGroup.SetActive(true);
        }

        public void Hide()
        {
            canvasGroup.SetActive(false);
        }

        private void Update()
        {
            if (characterStats == null)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("Attk: ").Append(characterStats.AttackDamage).Append("\n");
            builder.Append("Crit Dmg: ").Append(characterStats.CriticalDamageStat.Value).Append("\n");
            builder.Append("Crit % : ").Append(characterStats.CriticalChanceStat.Value).Append("\n");
            builder.Append("Attk Speed: ").Append(characterStats.ManualAttackRateStat.Value).Append("\n");
            builder.Append("Magic Dmg: ").Append(characterStats.MagicDamageStat.Value).Append("\n");
            builder.Append("Mag Resist: ").Append(characterStats.MagicalResistanceStat.Value).Append("\n");
            builder.Append("Endur Regen: ").Append(characterStats.EnduranceRegenStat.Value).Append("\n");
            builder.Append("Evasion % : ").Append(characterStats.EvasionChanceStat.Value).Append("\n");
            builder.Append("Item Drop % : ").Append(characterStats.ItemDropRateStat.Value).Append("\n");
            statsText.text = builder.ToString();
        }
    }
}