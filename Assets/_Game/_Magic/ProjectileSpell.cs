using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "Magic/SpecialSpell/DefaultProjectile")]
public class ProjectileSpell : MagicSpell
{
    [SerializeField, Tooltip("Portée max du sort")]
    protected float _range; public float Range => _range;

    [SerializeField, Tooltip("Vitesse de déplacement du sort")]
    protected float _speed; public float Speed => _speed;

    [SerializeField]
    protected GameObject defaultProjectile;

    [SerializeField]
    protected Transform originCast;

    public override void Cast(MagicSpell projectileSpell)
    {
        if(originCast == null) originCast = FindObjectOfType<CharacterMovement>().transform.Find("CastSpawn");

        GameObject projectile = Instantiate(defaultProjectile);
        projectile.transform.position = originCast.position;
        if(spellSprite != null) projectile.GetComponent<SpriteRenderer>().sprite = spellSprite;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(originCast.transform.right * -1 * _speed);

        projectile.GetComponent<ProjectileController>().ProjectileSpell = (ProjectileSpell)projectileSpell;
    }

    virtual public void Impact(Collider2D collision, GameObject projectile)
    {
        Debug.Log("Impact");
    }
}