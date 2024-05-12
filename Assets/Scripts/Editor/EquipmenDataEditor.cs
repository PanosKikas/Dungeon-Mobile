using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DMT.Character.Stats;
using System.Reflection;

[CustomEditor(typeof(EquipmentData), true)]
public class EquipmenDataEditor : Editor
{
    EquipmentData equipment;
    
    private void OnEnable()
    {
        equipment = (EquipmentData)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, GetVariables());
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Equipment Type");
        var selectedType = EditorGUILayout.EnumPopup(equipment.EquipmentType);
        equipment.EquipmentType = (EquipmentType)selectedType;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
       
        EditorGUILayout.LabelField("Modifiers List");
        EditorGUILayout.Space();
        if (equipment.Modifiers != null)
        {
            for (int i = equipment.Modifiers.Count - 1; i >= 0; --i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel++;
                var modifiers = equipment.Modifiers[i];
                var selectedStat = (StatType)EditorGUILayout.EnumPopup(modifiers.StatType);
                var selectedValue = EditorGUILayout.FloatField(modifiers.Value);

                equipment.Modifiers[i].StatType = selectedStat;
                equipment.Modifiers[i].Value = selectedValue;

                if (GUILayout.Button("-", GUILayout.Width(30)))
                {

                    equipment.Modifiers.Remove(modifiers);

                }


                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();

            }
        }
        
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            
            equipment.Modifiers.Add(new EquipmentData.StatValuePair(StatType.MaxHealth, 20));

        }

        serializedObject.ApplyModifiedProperties();
    }

    private string[] GetVariables()
    {
        List<string> variables = new List<string>();
        BindingFlags bindingFlags = BindingFlags.DeclaredOnly | // This flag excludes inherited variables.
                                    BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Instance |
                                    BindingFlags.Static;
        foreach (FieldInfo field in typeof(EquipmentData).GetFields(bindingFlags))
        {
            variables.Add(field.Name);
        }
        return variables.ToArray();
    }
}
