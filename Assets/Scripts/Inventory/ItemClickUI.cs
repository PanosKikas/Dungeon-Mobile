using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class IndexEvent : UnityEvent<int> { }

public class ItemClickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 

    bool pointerDown = false;

    float pointerDownTimer = 0f;

    [SerializeField]
    float requiredHoldTime;

    public IndexEvent OnLongClick;
    public IndexEvent OnTap;
    

    [SerializeField]
    private Image fillImage;

    void Awake()
    {
        OnLongClick = new IndexEvent();
        OnTap = new IndexEvent();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {   
        if (pointerDownTimer < requiredHoldTime)
        {
            int index = transform.GetSiblingIndex();
            OnTap?.Invoke(index);
        }
        Reset();
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                OnLongClick?.Invoke(transform.GetSiblingIndex());
                //this.enabled = false;
                Reset();
            }
            
            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0f;
        fillImage.fillAmount = 0;
    }
}
