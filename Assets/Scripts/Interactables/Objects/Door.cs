using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    public List<Condition> pendingConditions;

    public void Start()
    {
        foreach (Condition condition in pendingConditions) 
        {
            condition.OnComplete += ConditionCompleteHandler;
        }
    }

    private void ConditionCompleteHandler(Condition condition)
    {
        if (isOpen)
        {
            return;
        }
        
        pendingConditions.Remove(condition);
        if(!pendingConditions.Any())
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    public void CloseDoor()
    {

    }

    public void ToggleDoor()
    {

    }
}
