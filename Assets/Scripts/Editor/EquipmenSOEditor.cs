using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(EquipableSO), true)]
public class EquipmenSOEditor : Editor
{
    EquipableSO equipable;
    bool foldout;
    private void OnEnable()
    {
        equipable = (EquipableSO)target;
        
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
                var selectedStat = (Stat)EditorGUILayout.EnumPopup(modifiers.stat);
                var selectedValue = EditorGUILayout.FloatField(modifiers.value);

                equipable.StatValuePairs[i].stat = selectedStat;
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
            
            equipable.StatValuePairs.Add(new EquipableSO.StatValuePair(Stat.MaxHealth, 20));

        }


        serializedObject.ApplyModifiedProperties();
    }
}
