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

    [SerializeField, Tooltip("Si le joueur peut prendre des dégâts")]
    private bool _isInvincible = false;

    [SerializeField, Tooltip("Temps d'invincibilité du joueur")]
    private float _invincibilityTime = 2;

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

    public override void PhysicalDamage(int damage)
    {
        base.PhysicalDamage(damage);

        if(!_isInvincible && _HP > 0)
        {
            StartCoroutine(InvincibilityCoroutine());
            StartCoroutine(BlinkCoroutine());
        }
    }
    protected override void Death()
    {
        levelManager.GameOver();
    }

    /// <summary>
    /// Override de la modification de la vie
    /// </summary>
    /// <param name="modifLife">Valeur de modification de la vie</param>
    protected override void SetLife(int modifLife)
    {
        if(!_isInvincible)
            base.SetLife(modifLife);

        healthBar.SetHealth(_HP);
    }
    /// <summary>
    /// Override de la modification de mana
    /// </summary>
    /// <param name="mana">Valeur de modification de mana</param>
    public override void SetMana(int mana)
    {
        base.SetMana(mana);
        manaBar.SetHealth(_mana);
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibilityTime);
        _isInvincible = false;
    }

    private IEnumerator BlinkCoroutine()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        while(_isInvincible)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.25f);
            yield return new WaitForSeconds(0.2f);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            yield return new WaitForSeconds(0.2f);
        }
    }
}