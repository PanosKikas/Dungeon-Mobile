using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DMT.Battle
{
    public class RandomTargetSelector : ITargetSelector
    {
        public BattleCharacter SelectFrom(IEnumerable<BattleCharacter> characters)
        {
            var characterArray = characters.ToArray();
            var index = Random.Range(0, characterArray.Count());
            return characterArray.ElementAt(index);
        }
    }
}