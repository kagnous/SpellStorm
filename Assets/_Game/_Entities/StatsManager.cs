using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField, Tooltip("Speed")]
    private float _speed = 100; public float Speed { get { return _speed; } set { _speed = value; } }

    [SerializeField, Tooltip("Jump intensity")]
    private float _jumpForce = 300; public float JumpForce { get { return _jumpForce; } set { _jumpForce = value; } }

    [SerializeField]
    protected int _maxHP = 10; public int MaxHP { get { return _maxHP; } set { _maxHP = value; } }
    [SerializeField]
    protected int _HP; public int HP { get { return _HP; } set { _HP = value; } }

    [SerializeField]
    protected int _maxMana = 100; public int MaxMana { get { return _maxMana; } set { _maxMana = value; } }
    [SerializeField]
    protected int _mana; public int Mana { get { return _mana; } set { _mana = value; } }

    [SerializeField, Tooltip("Réduction de dégâts physiques")]
    private int _armor; public int Armor { get { return _armor; } set { _armor = value; } }

    [SerializeField, Tooltip("Tout les effets qui affectent l'entité")]
    List<EffectController> _effects; public List<EffectController> Effects { get { return _effects; } set { _effects = value; } }

    protected virtual void Awake()
    {
        _HP = _maxHP;
        _mana = _maxMana;
        _effects = new List<EffectController>();
    }

    // Ajoute un effet à l'entité
    public void AddEffect(EffectMother effect)
    {
        // Teste si l'effet est pas déjà actif
        for (int i = 0; i < _effects.Count; i++)
        {
            if (effect == _effects[i].Effet)
            {
                _effects[i].RefreshEffect();
                return;
            }
        }

        #region Test type contraires
        if (effect.TypeOfTheEffect == EffectMother.TypeEffect.Cold)
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                if (_effects[i].TypeOfTheEffect == EffectMother.TypeEffect.Fire)
                {
                    Debug.Log("Froid enlève feu");
                    _effects[i].EndEffect();
                    return;
                }
            }
        }
        else if (effect.TypeOfTheEffect == EffectMother.TypeEffect.Fire)
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                if (_effects[i].TypeOfTheEffect == EffectMother.TypeEffect.Cold)
                {
                    Debug.Log("Feu enlève froid");
                    _effects[i].EndEffect();
                    return;
                }
            }
        }
        #endregion

        GameObject effectObject = effect.Apply(this);
        _effects.Add(effectObject.GetComponent<EffectController>());
    }

    /// <summary>
    /// Met a jour la vie après être passée par les modificateurs armure/éléments
    /// </summary>
    /// <param name="modifLife">La valeur a appliquer à la vie</param>
    protected virtual void SetLife(int modifLife)
    {
        _HP += modifLife;
        if(_HP <= 0)
        {
            Debug.Log(name + " meurt !");
            Destroy(gameObject);
        }
        else if(_HP > _maxHP)
        {
            _HP = _maxHP;
        }
        Debug.Log($"{name} a {_HP} hp");
    }

    /// <summary>
    /// Calcule les dégâts physiques reçus en fonction de l'armure
    /// </summary>
    /// <param name="damage">Dégâts reçus</param>
    public void PhysicalDamage(int damage)
    {
        damage -= _armor;
        if (damage <= 0)
        {
            damage = 1;
        }
        SetLife(-damage);
    }

    /// <summary>
    /// Calcule les dégâts élémentaire selon les résistances
    /// </summary>
    /// <param name="damage">Dégâts reçus</param>
    public void ElementalDamage(int damage)
    {
        SetLife(-damage);
    }

    /// <summary>
    /// Calcule la quantité de soin gagné
    /// </summary>
    /// <param name="heal">Soin reçus</param>
    public void Heal(int heal)
    {
        SetLife(heal);
    }

    public virtual void SetMana(int mana)
    {
        _mana += mana;
        if(_mana > _maxMana)
        {
            _mana = _maxMana;
        }
        else if(_mana < 0)
        {
            _mana = 0;
        }
    }
}