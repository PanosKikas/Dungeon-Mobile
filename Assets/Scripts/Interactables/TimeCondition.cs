using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeCondition : Condition
{
    private List<Condition> pendingConditions;

    [SerializeField]
    private List<Condition> startingConditions;

    private float Countdown;
    private bool isTimerRunning;
    public float StartingTime;

    private void ResetConditions()
    {
        pendingConditions = new List<Condition>(startingConditions);
        isTimerRunning = false;
        Countdown = StartingTime;
    }

    public void Start()
    {
        ResetConditions();

        foreach (Condition condition in startingConditions)
        {
            condition.OnComplete += OnConditionComplete;
        }
    }

    public void Update()
    {
        if (!isTimerRunning)
        {
            return;
        }
        Countdown -= Time.deltaTime;
        if (Countdown <= 0)
        {
            ResetConditions();
        }
    }

    private void OnConditionComplete(Condition condition)
    {
        pendingConditions.Remove(condition);

        if (!isTimerRunning)
        {
            isTimerRunning = true;
        }

        if (!pendingConditions.Any() && Countdown > 0)
        {
            Complete();
        }
    }


}
