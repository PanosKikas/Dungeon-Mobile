using UnityEngine;

[System.Serializable]
public class Experience 
{
    public int CurrentLevel;
    private int currentXp;
    private int expBase;
    private int expLeft;
    private float ExpMod = 1.15f;

    public Experience()
    {
        CurrentLevel = 1;
        currentXp = 0;
        expBase = 10;
        expLeft = expBase;
    }

    public void GainExp(int amount)
    {
        currentXp += amount;
        if (currentXp >= expLeft)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXp -= expLeft;
        CurrentLevel++;
        float t = Mathf.Pow(ExpMod, CurrentLevel);
        expLeft = Mathf.FloorToInt(t * expBase);
    }
}
