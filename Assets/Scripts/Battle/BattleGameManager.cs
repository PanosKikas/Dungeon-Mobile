using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BattleGameManager : MonoBehaviour
{
   
    [SerializeField]
    PlayerBattleFSM[] playersFSM;

    PlayerBattleFSM currentlySelectedCharacter;
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
        currentlySelectedCharacter = playersFSM[currentlySelectedIdx];
        ActivateManualCombat();

        for (int i = 1; i < playersFSM.Length; i++)
        {
            DeselectCharacter(i);
        }
    }
    

    public void SelectCharacter(int idx)
    {
        PlayerBattleFSM characterToSelect = playersFSM[idx];

        if (currentlySelectedCharacter != characterToSelect)
        {
            DeselectCharacter(currentlySelectedIdx);
            currentlySelectedIdx = idx;
            currentlySelectedCharacter = playersFSM[idx];
            ActivateManualCombat();
        }
    }

    void ActivateManualCombat()
    {
        currentlySelectedCharacter.ChangeState(currentlySelectedCharacter.ManualAttackState);
        currentlySelectedCharacter.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
    }

    void DeselectCharacter(int idx)
    {
        PlayerBattleFSM characterToDeselect = playersFSM[idx];
        characterToDeselect.ChangeState(characterToDeselect.AutoAttackState);
        characterToDeselect.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
    }
    
}
