using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoneThrow", menuName = "Magic/Spell/StoneThrow")]
public class StoneThrow : ProjectileSpell
{
    [SerializeField, Tooltip("Dégâts infligés")]
    private int _damage; public int Damage => _damage;

    public override void PostCast(GameObject projectile)
    {
        projectile.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
    }

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        if (collision.gameObject.tag == "Mob" || collision.gameObject.tag == "Player")
            collision.GetComponent<StatsManager>().PhysicalDamage(_damage);
    }
}
//SerializedReference