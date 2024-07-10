using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TalkBehaviour : MonoBehaviour, IPointerClickHandler
{
    [FormerlySerializedAs("DialogueBoxPrefab")] [SerializeField]
    private DialogueSystem dialogueBoxPrefab;

    private bool isTalking;

    [SerializeField]
    private readonly Vector3 spawnOffset = new(2, .8f, 0);

    public DialogueText Dialogue;
    CharacterTopDownManager mainPlayerTopDown;

    public UnityEvent OnComplete = new();
    
    void Awake()
    {
        mainPlayerTopDown = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterTopDownManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isTalking)
        {
            return;
        }

        mainPlayerTopDown.Freeze();
        DialogueSystem spawnedDialogue = Instantiate(dialogueBoxPrefab, transform.position, Quaternion.identity);
        spawnedDialogue.transform.SetParent(transform, true);
        RectTransform rect = spawnedDialogue.GetComponent<RectTransform>();
        rect.position = transform.position + spawnOffset;
        spawnedDialogue.Show(Dialogue, () => StartCoroutine(OnDialogueComplete()));
        isTalking = true;
    }

    IEnumerator OnDialogueComplete()
    {
        yield return new WaitForSeconds(0.1f);
        isTalking = false;
        mainPlayerTopDown.Unfreeze();
        OnComplete?.Invoke();
    }
}
