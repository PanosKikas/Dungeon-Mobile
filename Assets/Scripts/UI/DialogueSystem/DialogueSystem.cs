using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dialogueTextUI;

    private float speed = 0.025f;

    [HideInInspector]
    public DialogueText dialogueText;

    private int currentLineIndex = 0;

    private bool awaitingClick = false;

    string currentLine;
    string kAlphaCode = "<color=#00000000>";
    Action OnComplete;

    public void Show(DialogueText dialogue, Action onCompleteCallback=null)
    {
        OnComplete = onCompleteCallback;
        this.dialogueText = dialogue;
        dialogueTextUI.text = string.Empty;
        if (currentLineIndex < dialogueText.Text.Count)
        {
            StartCoroutine(AnimateNextLine());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (awaitingClick)
            {
                if (currentLineIndex < dialogueText.Text.Count - 1)
                {
                    ++currentLineIndex;
                    awaitingClick = false;
                    StartCoroutine(AnimateNextLine());
                }
                else
                {
                    OnComplete?.Invoke();
                    Destroy(gameObject);
                }
            }
            else
            {
                ShowCompletedLine();
            }
        }
    }

    private void ShowCompletedLine()
    {
        StopAllCoroutines();
        dialogueTextUI.text = currentLine;
        awaitingClick = true;
    }

    IEnumerator AnimateNextLine()
    {
        dialogueTextUI.text = string.Empty;
        currentLine = dialogueText.Text[currentLineIndex];
        string originalText = currentLine;
        string displayedText = "";
        int alphaIndex = 0;
        for (int i = 0; i < currentLine.Length; ++i)
        {
            ++alphaIndex;
            if (awaitingClick)
            {
                yield break;
            }
            dialogueTextUI.text = originalText;
            displayedText = dialogueTextUI.text.Insert(alphaIndex, kAlphaCode);
            dialogueTextUI.text = displayedText;
            dialogueTextUI.text += currentLine[i];
            yield return new WaitForSeconds(speed);
        }
        awaitingClick = true;
    }


}
