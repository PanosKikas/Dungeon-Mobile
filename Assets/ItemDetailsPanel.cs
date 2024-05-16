using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    public void ShowDetailsFor(IStorable item)
    {
        itemName.text = item.Name;
        itemDescription.text = item.Description;
    }

    public void EmptyDescription()
    {
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
    }
}
