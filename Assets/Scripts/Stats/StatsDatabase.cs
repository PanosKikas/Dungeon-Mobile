using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDatabase : MonoBehaviour
{

    [SerializeField]
    private List<PlayerCharacterStats> PlayerCharacterStats;

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


    public PlayerCharacterStats GetMainCharacterStats()
    {
        return PlayerCharacterStats[0];
    }

    public PlayerCharacterStats GetSideCharacterStats(int index)
    {
        return PlayerCharacterStats[index + 1];
    }

    public PlayerCharacterStats GetCharacterStats(int index)
    {
        return PlayerCharacterStats[index];
    }
   
}
