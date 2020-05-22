using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDatabase : MonoBehaviour
{

    public PlayerCharacterStats[] PlayerCharacterStats;
    public PlayerStatusEffects[] PlayerStatusEffects;

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

    private void Start()
    {
        PlayerCharacterStats[0].Initialize();
    }
   
}
