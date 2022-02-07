using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "Magic/SpecialSpell/DefaultProjectile")]
public class ProjectileSpell : MagicSpell
{
    [SerializeField, Tooltip("Port�e max du sort")]
    protected float _range; public float Range => _range;

    [SerializeField, Tooltip("Vitesse de d�placement du sort")]
    protected float _speed; public float Speed => _speed;

    [SerializeField]
    protected GameObject defaultProjectile;

    public override void Cast(MagicSpell spell, GameObject caster)
    {
        //CharacterCasting characterCasting = caster.GetComponent<CharacterCasting>();
        //if (characterCasting.CanCast)
        {
            Transform originCast = caster.transform.Find("CastSpawn");

            GameObject projectile = Instantiate(defaultProjectile);
            projectile.transform.position = originCast.position;
            if (spellSprite != null) projectile.GetComponent<SpriteRenderer>().sprite = spellSprite;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(originCast.transform.right * -1 * _speed);

            ProjectileController controller = projectile.GetComponent<ProjectileController>();
            controller.ProjectileSpell = (ProjectileSpell)spell;
            controller.Caster = caster;

            //characterCasting.CanCast = false;
        }
    }

    /// <summary>
    /// Fonction appel�e par le ProjectileController en cas d'impact avec autre chose que les tags Player et Magic
    /// </summary>
    /// <param name="collision">La collision rapport�e par le projectile</param>
    /// <param name="projectile">L'object projectile lui m�me</param>
    virtual public void Impact(Collider2D collision, GameObject projectile)
    {

    }

    public override void EndSpell(MagicSpell projectileSpell, GameObject caster)
    {
        //caster.GetComponent<CharacterCasting>().CanCast = true;
        //Debug.Log("Tir ok");
    }
}