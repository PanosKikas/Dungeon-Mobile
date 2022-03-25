using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{

    public List<Condition> pendingConditions;

    public void Start()
    {
        foreach (Condition condition in pendingConditions) {
            condition.OnComplete += ConditionCompleteHandler;
        }
    }

    private void ConditionCompleteHandler(Condition condition)
    {
        pendingConditions.Remove(condition);
        if(!pendingConditions.Any()){
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        Destroy(gameObject);
    }

    public void CloseDoor()
    {

    }

    public void ToggleDoor()
    {

    }
}
