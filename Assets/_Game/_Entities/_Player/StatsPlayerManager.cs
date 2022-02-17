using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KagnousLib;

public class StatsPlayerManager : StatsManager
{
    private HealthBar healthBar;
    private HealthBar manaBar;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        // Recup et set barre de vie
        healthBar = FindObjectsOfType<HealthBar>()[0];
        healthBar.SetMaxHealth(_maxHP);
        // Recup et set barre de mana
        manaBar = FindObjectsOfType<HealthBar>()[1];
        manaBar.SetMaxHealth(_maxMana);
    }

    protected override void SetLife(int modifLife)
    {
        base.SetLife(modifLife);
        healthBar.SetHealth(_HP);
        if(_HP <= 0)
        {
            levelManager.GameOver();
        }
    }

    public override void SetMana(int mana)
    {
        base.SetMana(mana);
        manaBar.SetHealth(_mana);
    }
}