using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] attributeValues;
    Attribute[] attributes;

    Button[] attributeButtons;

    private void OnEnable()
    {
        if (attributeButtons == null)
        {
            attributeButtons = GetComponentsInChildren<Button>();
            foreach (Button button in attributeButtons)
            {
                button.onClick.AddListener(() => IncrementAttribute(button.transform.GetSiblingIndex()));
            }
        }
    }

    private void Start()
    {
        attributes = StatsDatabase.Instance.GetMainCharacterStats().stats.AttributeStats;
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
