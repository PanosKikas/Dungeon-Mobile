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

    public void ShowFor(string characterId)
    {
        characterCamera.enabled = true;
        animator.runtimeAnimatorController = CharacterUtils.GetCharacterAnimatorController(characterId);
    }

    public void Hide()
    {
        characterCamera.enabled = false;
    }
}