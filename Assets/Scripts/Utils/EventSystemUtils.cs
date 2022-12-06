using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public static class EventSystemUtils 
{
    public static bool IsPointerOverUI(this EventSystem eventSystem)
    {
        var pointerData = new PointerEventData(eventSystem);
        pointerData.position = Mouse.current.position.ReadValue();
        var results = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerData, results);
        foreach (var hit in results)
        {
            if (hit.gameObject != null 
                && hit.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }

        return false;
    }
}
