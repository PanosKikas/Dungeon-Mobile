using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BattleGameManager : MonoBehaviour
{
    [SerializeField] private CharacterBattleController[] Characters;

    CharacterBattleController currentlySelectedCharacter;
    int currentlySelectedIdx;

    private void Start()
    {     
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {   
        yield return new WaitForSeconds(2f);

        currentlySelectedIdx = 0;
        currentlySelectedCharacter = Characters[currentlySelectedIdx];
        ActivateManualCombat();

        for (int i = 1; i < Characters.Length; i++)
        {
            DeselectCharacter(i);
        }
    }

    public void SelectCharacter(int idx)
    {
        var characterToSelect = Characters[idx];

        if (currentlySelectedCharacter != characterToSelect)
        {
            DeselectCharacter(currentlySelectedIdx);
            currentlySelectedIdx = idx;
            currentlySelectedCharacter = Characters[idx];
            ActivateManualCombat();
        }
    }

    void ActivateManualCombat()
    {
        currentlySelectedCharacter.Select();
        currentlySelectedCharacter.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
    }

    void DeselectCharacter(int idx)
    {
        var characterToDeselect = Characters[idx];
        characterToDeselect.Deselect();
        characterToDeselect.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
    }
}
