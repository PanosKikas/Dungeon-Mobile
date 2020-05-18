using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.Rendering.Universal;

public class BattleGameManager : MonoBehaviour
{
   
    [SerializeField]
    PlayerBattle[] aliveCharacters;

    PlayerBattle currentlySelectedCharacter;
    int currentlySelectedIdx;

    #region Singletton
    public static BattleGameManager _instance;

    public static BattleGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BattleGameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("BattleGameManager");
                    _instance = container.AddComponent<BattleGameManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    private void Start()
    {     
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {   
        yield return new WaitForSeconds(2f);

        currentlySelectedIdx = 0;
        currentlySelectedCharacter = aliveCharacters[currentlySelectedIdx];
        ActivateManualCombat();
        for (int i = 1; i < aliveCharacters.Length; i++)
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
            ActivateManualCombat();
        }
    }

    void ActivateManualCombat()
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
