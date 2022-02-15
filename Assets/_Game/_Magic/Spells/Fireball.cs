using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFireball", menuName = "Magic/Spell/Fireball")]
public class Fireball : ProjectileSpell
{
    [SerializeField, Tooltip("Dégâts infligés")]
    private int _damage; public int Damage => _damage;

    [SerializeField, Tooltip("Rayon d'explosion")]
    private float _damageRange; public float DamageRange => _damageRange;

    [SerializeField]
    private LayerMask _collisionLayer;

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        // Cibles dans la zone d'explosion
        Collider2D[] cibles =  Physics2D.OverlapCircleAll(projectile.transform.position, _damageRange, _collisionLayer);

        Debug.Log("Nombre de cibles : " + cibles.Length);
        for (int i = 0; i < cibles.Length; i++)
        {
            if (cibles[i].gameObject.tag == "Mob" || cibles[i].gameObject.tag == "Player")
                cibles[i].GetComponent<StatsManager>().ElementalDamage(_damage);
        }
    }
}
//SerializedReference