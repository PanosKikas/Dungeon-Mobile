namespace EventArgs
{
    public class CharacterDamagedArgs
    {
        public int CurrentHp;
        public int MaxHp;

        public CharacterDamagedArgs(int currentHp, int maxHp)
        {
            CurrentHp = currentHp;
            MaxHp = maxHp;
        }
    }
}