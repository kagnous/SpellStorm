using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KagnousLib;

public class StatsPlayerManager : StatsManager
{
    private HealthBar healthBar;
    private HealthBar manaBar;

    private LevelManager levelManager;

    [Header("")]
    [SerializeField, Tooltip("Quantité de mana régénérée par X s")]
    private int _manaPerX = 1;

    [SerializeField, Tooltip("Intervalle de temps du régèn de mana")]
    private float _manaRegenTime = 1;

    private float timer = 0;

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

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= _manaRegenTime)
        {
            SetMana(_manaPerX);
            timer = 0;
        }
    }

    /// <summary>
    /// Override de la modification de la vie
    /// </summary>
    /// <param name="modifLife">Valeur de modification de la vie</param>
    protected override void SetLife(int modifLife)
    {
        _HP += modifLife;
        if (_HP <= 0)
        {
            levelManager.GameOver();
        }
        else if (_HP > _maxHP)
        {
            _HP = _maxHP;
        }
        Debug.Log($"Le joueur a {_HP} hp");
        healthBar.SetHealth(_HP);
    }

    public override void SetMana(int mana)
    {
        base.SetMana(mana);
        manaBar.SetHealth(_mana);
    }
}