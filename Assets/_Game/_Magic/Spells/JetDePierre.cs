using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJetDePierre", menuName = "Magic/SpecialSpell/JetDePierre")]
public class JetDePierre : ProjectileSpell
{
    [SerializeField, Tooltip("Dégâts infligés")]
    private int _damage; public int Damage => _damage;

    public override void Cast(MagicSpell spell, GameObject caster)
    {
        ///////////////////////   Trouver comment garder base.Cast() tout en pouvant chopper rb   ///////////////////////////////////////////
        Transform originCast = caster.transform.Find("CastSpawn");

        GameObject projectile = Instantiate(defaultProjectile);
        projectile.transform.position = originCast.position;
        if (spellSprite != null) projectile.GetComponent<SpriteRenderer>().sprite = spellSprite;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(originCast.transform.right * -1 * _speed);

        projectile.GetComponent<ProjectileController>().ProjectileSpell = (ProjectileSpell)spell;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        rb.gravityScale = 0.3f;
    }

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        if (collision.gameObject.tag == "Mob")
            collision.GetComponent<StatsManager>().SetLife(-_damage);
    }
}
//SerializedReference