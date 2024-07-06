using System.Collections.Generic;

namespace DMT.Battle
{
    public interface ITargetSelector
    {
        BattleCharacter Select(IEnumerable<BattleCharacter> characters);
    }
}