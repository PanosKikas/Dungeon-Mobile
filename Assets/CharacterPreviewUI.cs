using DMT.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterPreviewUI : MonoBehaviour
{
    [SerializeField]
    private HPBarUI hpBar;

    [SerializeField]
    private TextMeshProUGUI levelText;

    public void SetTo(Character character)
    {
        hpBar.SetToStats(character.stats);
        levelText.text = string.Format($"Level {character.Level}");
        string path = @"Assets/Animations/Character/" + character.Name;
        string assetFound = AssetDatabase.FindAssets("t:AnimatorController", new string[] { path }).FirstOrDefault();
        var animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GUIDToAssetPath(assetFound));
       // animator.runtimeAnimatorController = animatorController;
    }
}
