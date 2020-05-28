using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttributeUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] attributeValues;
    Attribute[] attributes;

    private void Start()
    {
        attributes = StatsDatabase.Instance.PlayerCharacterStats[0].attributes;
        UpdateAttributeTexts();
    }

    void UpdateAttributeTexts()
    {
        for (int i = 0; i < attributes.Length; ++i)
        {
            attributeValues[i].text = attributes[i].Value.ToString();
        }
    }

    public void IncrementAttribute(int index)
    {
        attributes[index].Value++;
        attributeValues[index].text = attributes[index].Value.ToString();
    }
}
