using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttributeButtonUI : MonoBehaviour
{
    AttributeUI ui;

    private void Awake()
    {
        ui = GetComponentInParent<AttributeUI>();
    }


    public void UpgradeAttribute()
    {
        ui.IncrementAttribute(transform.GetSiblingIndex());
    }
}
