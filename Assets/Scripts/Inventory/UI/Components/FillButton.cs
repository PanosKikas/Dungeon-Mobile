using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPointerDown = false;

    float pointerDownTimer = 0f;

    [SerializeField]
    float requiredHoldTime = 0.7f;

    public UnityEvent OnClicked = new UnityEvent();
    public UnityEvent OnFilled = new UnityEvent();

    [SerializeField]
    private Image fillImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isPointerDown)
        {
            return;
        }

        if (pointerDownTimer < requiredHoldTime)
        {
            int index = transform.GetSiblingIndex();
            OnClicked?.Invoke();
        }
        ResetFill();
    }

    private void Update()
    {
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                ResetFill();
                OnFilled?.Invoke();
            }

            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void ResetFill()
    {
        isPointerDown = false;
        pointerDownTimer = 0f;
        fillImage.fillAmount = 0;
    }
}
