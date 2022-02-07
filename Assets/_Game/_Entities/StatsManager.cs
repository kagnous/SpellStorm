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
    private int _armor; public int Armor { get { return _armor; } set { _armor = value; } }

    private void Awake()
    {
        _HP = _maxHP;
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