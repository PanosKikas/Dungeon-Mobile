using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class ItemClickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 
    bool pointerDown = false;

    float pointerDownTimer = 0f;

    [SerializeField]
    float requiredHoldTime;

    public event Action OnLongClick;
    public event Action OnTap;

    [SerializeField]
    private Image fillImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {   
        if (pointerDownTimer < requiredHoldTime)
        {
            int index = transform.GetSiblingIndex();
            OnTap?.Invoke();
        }
        ResetClick();
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                OnLongClick?.Invoke();
                ResetClick();
            }
            
            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void ResetClick()
    {
        pointerDown = false;
        pointerDownTimer = 0f;
        fillImage.fillAmount = 0;
    }
}
