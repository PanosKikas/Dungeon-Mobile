using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(EquipmentSO), true)]
public class EquipmenSOEditor : Editor
{
    EquipmentSO equipable;
    
    private void OnEnable()
    {
        equipable = (EquipmentSO)target;
        
    }

    public override void OnInspectorGUI()
    {
        
        serializedObject.Update();
        base.OnInspectorGUI();
        EditorGUILayout.Space();
       

        EditorGUILayout.LabelField("Modifiers List");
        EditorGUILayout.Space();
        if (equipable.StatValuePairs != null)
        {
            for (int i = equipable.StatValuePairs.Count - 1; i >= 0; --i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel++;
                var modifiers = equipable.StatValuePairs[i];
                var selectedStat = (StatType)EditorGUILayout.EnumPopup(modifiers.statType);
                var selectedValue = EditorGUILayout.FloatField(modifiers.value);

                equipable.StatValuePairs[i].statType = selectedStat;
                equipable.StatValuePairs[i].value = selectedValue;

                if (GUILayout.Button("-", GUILayout.Width(30)))
                {

                    equipable.StatValuePairs.Remove(modifiers);

                }


                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();

            }
        }
        
        
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            
            equipable.StatValuePairs.Add(new EquipmentSO.StatValuePair(StatType.MaxHealth, 20));

        }


        serializedObject.ApplyModifiedProperties();
    }
}
