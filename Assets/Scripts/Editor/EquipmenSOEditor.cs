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
        if (equipable.Modifiers != null)
        {
            for (int i = equipable.Modifiers.Count - 1; i >= 0; --i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel++;
                var modifiers = equipable.Modifiers[i];
                var selectedStat = (Stat)EditorGUILayout.EnumPopup(modifiers.stat);
                var selectedValue = EditorGUILayout.FloatField(modifiers.value);

                equipable.Modifiers[i].stat = selectedStat;
                equipable.Modifiers[i].value = selectedValue;

                if (GUILayout.Button("-", GUILayout.Width(30)))
                {

                    equipable.Modifiers.Remove(modifiers);

                }


                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();

            }
        }
        
        
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            
            equipable.Modifiers.Add(new EquipableSO.StatValuePair(Stat.MaxHealth, 20));

        }


        serializedObject.ApplyModifiedProperties();
    }
}
