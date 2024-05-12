using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterUIPanel : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private InventoryUI inventoryUI;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private CharacterPageUI[] characterPages;

    private void Start()
    {
        Hide();
        var charactersInParty = player.Characters.ToArray();
        for (int i = 0; i < charactersInParty.Length; ++i)
        {
            characterPages[i].SetTo(charactersInParty[i]);
        }
        inventoryUI.Initialize(player.Inventory, charactersInParty);
    }

    public void Show()
    {
        canvasGroup.SetActive(true);
    }

    public void Hide()
    {
        canvasGroup.SetActive(false);
    }
}
