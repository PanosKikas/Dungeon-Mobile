using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggler : MonoBehaviour
{
    private bool CharacterUIEnabled = false;

    [SerializeField]
    CanvasGroup characterUI;

    [SerializeField]
    GameObject characterStats;

    private void Start()
    {
        characterUI.SetActive(false);
    }

    public void ToggleInventory()
    {
        characterUI.SetActive(!CharacterUIEnabled);
        CharacterUIEnabled = !CharacterUIEnabled;
    }

    public void ToggleCharacterStats()
    {
        characterStats.SetActive(!characterStats.activeSelf);
    }
    
}
