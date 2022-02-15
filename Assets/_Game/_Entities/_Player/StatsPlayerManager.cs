using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KagnousLib;

public class StatsPlayerManager : StatsManager
{
    private HealthBar healthBar;

    private void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(_maxHP);
    }

    protected override void SetLife(int modifLife)
    {
        base.SetLife(modifLife);
        healthBar.SetHealth(_HP);
    }
}