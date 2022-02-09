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
    private int _maxHP = 10; public int MaxHP { get { return _maxHP; } set { _maxHP = value; } }
    [SerializeField]
    private int _HP; public int HP { get { return _HP; } set { _HP = value; } }

    [SerializeField]
    private int _maxMana = 100; public int MaxMana { get { return _maxMana; } set { _maxMana = value; } }
    [SerializeField]
    private int _mana; public int Mana { get { return _mana; } set { _mana = value; } }

    [SerializeField, Tooltip("Réduction de dégâts physiques")]
    private int _armor; public int Armor { get { return _armor; } set { _armor = value; } }

    [SerializeField, Tooltip("Tout les effets qui affectent l'entité")]
    List<EffectMother> _effects; public List<EffectMother> Effects { get { return _effects; } set { _effects = value; } }

    private void Awake()
    {
        _HP = _maxHP;
        _mana = _maxMana;
        _effects = new List<EffectMother>();
    }

    public void AddEffect(EffectMother effect)
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            if (effect == _effects[i])
            {
                /////////////////////////// Trouver comment refresh l'effet (sachant qu'on accès qu'au TokenEffect et que c'est l'Object effect)/////////////////////
                return;
            }
        }
        _effects.Add(effect);
        effect.Apply(this);
    }

    virtual public void SetLife(int modifLife)
    {
        if(modifLife < 0)
        {
            modifLife += _armor;
            if(modifLife >= 0)
            {
                modifLife = -1;
            }
        }
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
}