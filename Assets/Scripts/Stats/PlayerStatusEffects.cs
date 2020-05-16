using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : StatusEffects
{
    public static int index;
    
 //   [SerializeField]
    //private PlayerCharacterStats playerStats;

//    public override CharacterStats stats => playerStats;

    private void Start()
    {
        
        
       // playerStats = StatsDatabase.Instance.PlayerCharacterStats[index++];

    }

    

    public static void DecreaseEndurance(PlayerCharacterStats playerStats)
    {
        playerStats.CurrentEndurance = Mathf.Clamp(playerStats.CurrentEndurance - playerStats.EndurancePerAttack,
                                            0, playerStats.MaxEndurace);
    }

    /*private void Update()
    {
        RechargeEndurance(Time.deltaTime * playerStats.EnduranceRechargeRate);
    }*/

    public static void RechargeEndurance(PlayerCharacterStats playerStats)
    {
        float endurance = Time.deltaTime * playerStats.EnduranceRechargeRate;
        playerStats.CurrentEndurance = Mathf.Clamp(playerStats.CurrentEndurance + endurance, 0f, playerStats.MaxEndurace);
    }

   

}
