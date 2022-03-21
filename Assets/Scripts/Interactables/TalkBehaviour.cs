using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TalkBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    DialogueSystem DialogueBoxPrefab;

    private bool isTalking = false;

    [SerializeField]
    private Vector3 SpawnOffset = new Vector3(2, 1, 0);

    public DialogueText Dialogue;
    CharacterTopDownManager mainPlayerTopDown;

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
        DialogueSystem spawnedDialogue = Instantiate<DialogueSystem>(DialogueBoxPrefab, transform.position, Quaternion.identity);
        spawnedDialogue.Show(Dialogue, () => StartCoroutine(OnDialogueComplete()));
        isTalking = true;
    }

    IEnumerator OnDialogueComplete()
    {
        yield return new WaitForSeconds(0.1f);
        isTalking = false;
        mainPlayerTopDown.Unfreeze();
    }
}
