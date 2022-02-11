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
    private int _maxMana = 100; public int MaxMana { get { return _maxMana; } set { _maxMana = value; } }
    [SerializeField]
    private int _mana; public int Mana { get { return _mana; } set { _mana = value; } }

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

    public void AddEffect(EffectMother effect)
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            if (effect == _effects[i].Effet)
            {
                _effects[i].RefreshEffect();
                return;
            }
        }
        if(effect.TypeOfTheEffect == EffectMother.TypeEffect.Cold)
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

        GameObject effectObject = effect.Apply(this);
        _effects.Add(effectObject.GetComponent<EffectController>());
    }

    public virtual void SetLife(int modifLife)
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

    public void PhysicalDamage(int damage)
    {
        damage -= _armor;
        if (damage <= 0)
        {
            damage = 1;
        }
        SetLife(-damage);
    }

    public void ElementalDamage(int damage)
    {
        SetLife(-damage);
    }

    public void Heal(int heal)
    {
        SetLife(-heal);
    }
}