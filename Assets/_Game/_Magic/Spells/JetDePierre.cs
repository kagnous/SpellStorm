using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJetDePierre", menuName = "Magic/SpecialSpell/JetDePierre")]
public class JetDePierre : ProjectileSpell
{
    [SerializeField, Tooltip("Dégâts infligés")]
    private int _damage; public int Damage => _damage;

    public override void Cast(MagicSpell jetDePierre)
    {
        base.Cast(jetDePierre);
    }

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        if (collision.gameObject.tag == "Mob")
            collision.GetComponent<StatsManager>().SetLife(-_damage);
    }
}
//SerializedReference