using System.Collections.Generic;
using DMT.Characters.Stats;
using UnityEngine;

namespace DMT.Characters.Stats
{
    [System.Serializable]
    public class StatModifiersData : MonoBehaviour
    {
        #region StatValuePair
        [System.Serializable]
        public class StatValuePair
        {
            public StatType StatType;
            public float Value;

            public StatValuePair(StatType stat, float value)
            {
                StatType = stat;
                Value = value;
            }
        }
        #endregion
        
        public List<StatValuePair> Modifiers = new();
    }
}