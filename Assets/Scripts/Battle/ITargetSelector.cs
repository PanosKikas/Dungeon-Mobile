using System.Collections.Generic;

namespace DMT.Battle
{
    public interface ITargetSelector
    {
        BattleCharacter SelectFrom(IEnumerable<BattleCharacter> characters);
    }
}