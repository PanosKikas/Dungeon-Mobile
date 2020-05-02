using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BattleGameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> aliveCharacters;


    GameObject currentlySelectedCharacter;
    int currentlySelectedIdx;

    private void Start()
    {
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        
        yield return new WaitForSeconds(2f);

        currentlySelectedIdx = 0;
        currentlySelectedCharacter = aliveCharacters[currentlySelectedIdx];
        MakeManualCombat();
        for (int i = 1; i < aliveCharacters.Count; i++)
        {

            DeselectCharacter(i);
        }

    }
    

    public void SelectCharacter(int idx)
    {
        GameObject characterToSelect = aliveCharacters[idx];
        if (currentlySelectedCharacter != characterToSelect)
        {
            DeselectCharacter(currentlySelectedIdx);
            currentlySelectedIdx = idx;
            currentlySelectedCharacter = aliveCharacters[idx];
            MakeManualCombat();
        }
    }

    void MakeManualCombat()
    {
        currentlySelectedCharacter.GetComponent<AutoAttack>().enabled = false;
        currentlySelectedCharacter.GetComponent<PlayerBattle>().enabled = true;
        currentlySelectedCharacter.GetComponentInChildren<Light2D>().enabled = true;
    }

    void DeselectCharacter(int idx)
    {
        GameObject characterToDeselect = aliveCharacters[idx];
        characterToDeselect.GetComponent<AutoAttack>().enabled = true;
        characterToDeselect.GetComponent<PlayerBattle>().enabled = false;
        characterToDeselect.GetComponentInChildren<Light2D>().enabled = false;
    }

}
