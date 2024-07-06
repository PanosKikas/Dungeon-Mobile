using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterPreviewAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Camera characterCamera;

    private Dictionary<string, RuntimeAnimatorController> _cachedAnimators = new();

    public void ShowFor(string characterId)
    {
        if (characterCamera)
        {
            characterCamera.enabled = true;
        }

        if (_cachedAnimators.TryGetValue(characterId, out var cachedAnimator))
        {
            animator.runtimeAnimatorController = cachedAnimator;
            return;
        }

        string path = "Assets/Animations/Character/" + characterId;
        string assetFound = AssetDatabase.FindAssets("t:RuntimeAnimatorController", new[] { path }).FirstOrDefault();
        var animatorController =
            AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(AssetDatabase.GUIDToAssetPath(assetFound));
        animator.runtimeAnimatorController = animatorController;
        _cachedAnimators[characterId] = animatorController;
    }

    public void Hide()
    {
        if (characterCamera)
        {
            characterCamera.enabled = false;
        }
    }
}