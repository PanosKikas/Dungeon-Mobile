﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : BattleUI
{

    RawImage fillImage;
    float effectSpeed = .2f;

    private void Awake()
    {
        fillImage = GetComponentInChildren<RawImage>();
    }


    protected override void Start()
    {
        base.Start();
        
        stats.OnHpLoss.AddListener(UpdateHealth);
        
    }    

    public void UpdateHealth()
    {
        bar.value = (float)stats.CurrentHealth / stats.MaxHealth;
    }

    private void Update()
    {
        Rect uvRect = fillImage.uvRect;
        uvRect.x -= Time.deltaTime * effectSpeed;
        fillImage.uvRect = uvRect;
    }
}
