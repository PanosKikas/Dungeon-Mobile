using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
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
        owner.CharacterDamaged += UpdateHealth;
    }    

    public void UpdateHealth(CharacterDamagedArgs damagedArgs)
    {
        bar.value = (float)damagedArgs.CurrentHp / damagedArgs.MaxHp;
    }

    private void Update()
    {
        Rect uvRect = fillImage.uvRect;
        uvRect.x -= Time.deltaTime * effectSpeed;
        fillImage.uvRect = uvRect;
    }
}
