using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using UnityEditor;
using UnityEngine;

public static class CharacterUtils 
{
    private static Dictionary<string, RuntimeAnimatorController> _cachedAnimators = new();

    public static RuntimeAnimatorController GetAnimatorController(this Character character)
    {
        return GetCharacterAnimatorController(character.Id);
    }

    public static RuntimeAnimatorController GetCharacterAnimatorController(string characterId)
    {
        if (_cachedAnimators.TryGetValue(characterId, out var cachedAnimator))
        {
            return cachedAnimator;
        }

        string path = "Assets/Animations/Character/" + characterId;
        string assetFound = AssetDatabase.FindAssets("t:RuntimeAnimatorController", new[] { path }).FirstOrDefault();
        var animatorController =
            AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(AssetDatabase.GUIDToAssetPath(assetFound));
        _cachedAnimators[characterId] = animatorController;
        return animatorController;
    }
}
