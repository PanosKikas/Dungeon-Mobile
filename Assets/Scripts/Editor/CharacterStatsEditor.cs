/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CharacterStats), true)]
public class CharacterStatsEditor : Editor
{
    CharacterStats CharacterStats;
    private const string CharacterStatsDictionaryName = "characterStats";
    private const string CharacterStatName = "Name";
    private const string CharacterStatValueName = "Value";

    SerializedProperty CharacterStatsDictionaryProperty;

    private void OnEnable()
    {
        CharacterStats = (CharacterStats)target;
        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }
        CharacterStatsDictionaryProperty = serializedObject.FindProperty(CharacterStatsDictionaryName);
    }

    public override void OnInspectorGUI()
    {
        //  base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Character Stats:");
        int i = 0;
        var statsCopy = new Dictionary<string, CharacterStat>(CharacterStats.characterStats);
        foreach (var key in CharacterStats.characterStats.Keys)
        {
            var stat = CharacterStats.characterStats[key];
            var searilizedStat = new SerializedObject(stat);

            EditorGUILayout.BeginHorizontal();
            EditorGUI.indentLevel++;

            var newName = EditorGUILayout.TextField(stat.Name);
            var newValue = EditorGUILayout.FloatField(stat.Value);

            if (newName != stat.Name)
            {
                statsCopy.Remove(key);
                statsCopy.Add(newName, new CharacterStat(newName, newValue));
            }
            else
            {
                stat.Value = newValue;
            }

            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                statsCopy.Remove(key);
            }
            i++;

            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
        }

        EditorGUI.indentLevel++;
        GUILayout.Space(10);

        if (GUILayout.Button("+", GUILayout.Width(30)))
        {

            statsCopy.Add("NewStat", new CharacterStat("NewStat", 0));

        }

        CharacterStats.characterStats = new Dictionary<string, CharacterStat>(statsCopy);
        serializedObject.ApplyModifiedProperties();
    }

    void RemoveItemFromCharacterDictionary(string key)
    {
        CharacterStats.characterStats.Remove(key);
    }


}

*/