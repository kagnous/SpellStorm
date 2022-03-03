using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFireball", menuName = "Magic/Spell/Fireball")]
public class Fireball : ProjectileSpell
{
    [SerializeField, Tooltip("Dégâts infligés")]
    private int _damage; public int Damage => _damage;

    [SerializeField, Tooltip("Type de dégâts infligés")]
    private EffectMother.TypeEffect _typeDamage;

    [SerializeField, Tooltip("Rayon d'explosion")]
    private float _damageRange; public float DamageRange => _damageRange;

    [SerializeField, Tooltip("Prefab du visuel d'explosion")]
    private GameObject _explosion;

    [SerializeField, Tooltip("Durée d'existence du visule d'explosion")]
    private float _explosionDuration = 2;

    [SerializeField, Tooltip("Colliders affectés par l'explosion")]
    private LayerMask _collisionLayer;

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        // Cibles dans la zone d'explosion
        Collider2D[] cibles =  Physics2D.OverlapCircleAll(projectile.transform.position, _damageRange, _collisionLayer);

        for (int i = 0; i < cibles.Length; i++)
        {
            if (cibles[i].gameObject.tag == "Mob" || cibles[i].gameObject.tag == "Player")
                cibles[i].GetComponent<StatsManager>().ElementalDamage(_damage);
            else if (cibles[i].gameObject.tag == "Reactive")
            {
                cibles[i].GetComponent<ReactiveManager>().SufferEffect(_typeDamage);
            }
        }
        // Graphisme d'explosion
        GameObject explosionToken = Instantiate(_explosion, projectile.transform.position, projectile.transform.rotation);
        Destroy(explosionToken, _explosionDuration);
    }
}
//SerializedReference