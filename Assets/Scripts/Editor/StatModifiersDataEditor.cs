/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DMT.Characters.Stats;
using System.Reflection;
using DMT.Characters;
using Items.Inventory;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(StatModifiersData))]
public class StatModifiersDataEditor : PropertyDrawer
{
    StatModifiersData data;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUI.LabelField(position, new GUIContent("Modifier List"));

        var modifiers = property.serializedObject.FindProperty("Modifiers");
        if (modifiers == null)
        {
            return;
        }
        
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            data.Modifiers.Add(new StatModifiersData.StatValuePair(StatType.MaxHealth, 0));
        }
        EditorGUI.EndProperty();
    }

    /*public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Modifiers List");
        EditorGUILayout.Space();
        if (data.Modifiers != null)
        {
            for (int i = data.Modifiers.Count - 1; i >= 0; --i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel++;
                var modifiers = data.Modifiers[i];
                var selectedStat = (StatType)EditorGUILayout.EnumPopup(modifiers.StatType);
                var selectedValue = EditorGUILayout.FloatField(modifiers.Value);

                data.Modifiers[i].StatType = selectedStat;
                data.Modifiers[i].Value = selectedValue;

                if (GUILayout.Button("-", GUILayout.Width(30)))
                {

                    data.Modifiers.Remove(modifiers);

                }


                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();

            }
        }
        
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            data.Modifiers!.Add(new StatModifiersData.StatValuePair(StatType.MaxHealth, 20));
        }
        
        serializedObject.ApplyModifiedProperties();
    }#1#
}
*/
