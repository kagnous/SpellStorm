using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFireball", menuName = "Magic/SpecialSpell/Fireball")]
public class Fireball : ProjectileSpell
{
    [SerializeField, Tooltip("D�g�ts inflig�s")]
    private int _damage; public int Damage => _damage;

    [SerializeField, Tooltip("Rayon d'explosion")]
    private float _damageRange; public float DamageRange => _damageRange;

    public override void Cast()
    {
        base.Cast();
        Debug.Log("Boule de feu !");
    }

    public override void Impact()
    {
        Debug.Log("Explosion de feu 8d6 d�g�ts");
    }
}