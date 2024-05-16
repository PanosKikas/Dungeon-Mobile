using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterPreviewAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Camera characterCamera;

    private Dictionary<string, RuntimeAnimatorController> _cachedAnimators = new();

    public void ShowFor(Character character)
    {
        characterCamera.enabled = true;
        if (_cachedAnimators.ContainsKey(character.Name))
        {
            animator.runtimeAnimatorController = _cachedAnimators[character.Name];
            return;
        }
        string path = @"Assets/Animations/Character/" + character.Name;
        string assetFound = AssetDatabase.FindAssets("t:RuntimeAnimatorController", new string[] { path }).FirstOrDefault();
        var animatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(AssetDatabase.GUIDToAssetPath(assetFound));
        animator.runtimeAnimatorController = animatorController;
        _cachedAnimators[character.Name] = animatorController;
    }

    public void Hide()
    {
        characterCamera.enabled = false;
    }
}
