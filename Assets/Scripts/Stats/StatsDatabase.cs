using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDatabase : MonoBehaviour
{
    
    public List<PlayerCharacterStatsSO> PlayerCharacterStats;

    #region Singletton
    public static StatsDatabase Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    

    public PlayerCharacterStatsSO GetMainCharacterStats()
    {
        return PlayerCharacterStats[0];
    }

    public PlayerCharacterStatsSO GetSideCharacterStats(int index)
    {
        return PlayerCharacterStats[index + 1];
    }

    public PlayerCharacterStatsSO GetCharacterStats(int index)
    {
        return PlayerCharacterStats[index];
    }
   
}
