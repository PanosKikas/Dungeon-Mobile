using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    List<Slider> hpBars;


    public void InitializeUI(List<GameObject> aliveCharacters)
    {
        foreach (var character in aliveCharacters)
        {
            CharacterStats stats = character.GetComponent<StatusEffects>().stats;

        }
    }

    void InitializeHealthBar(CharacterStats stats)
    {

    }
}
