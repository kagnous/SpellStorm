using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MagicSpell
{
    [SerializeField, Tooltip("Portée max du sort")]
    protected float _range; public float Range => _range;

    [SerializeField, Tooltip("Vitesse de déplacement du sort")]
    protected float _speed; public float Speed => _speed;

    [SerializeField, Tooltip("Prefab de projectile magique")]
    protected GameObject defaultProjectile;

    public override void Cast(GameObject caster)
    {
        //On récupère les coordonnées d'invocation du sort
        Transform originCast = caster.transform.Find("CastSpawn");

        //On instancie le projectile et on le place au point d'invocation
        GameObject projectile = Instantiate(defaultProjectile);
        projectile.transform.position = originCast.position;

        // On récupère son sprite, on le set on le met dans le bon sens
        if (spellSprite != null) projectile.GetComponent<SpriteRenderer>().sprite = spellSprite;
        if (caster.transform.localScale.x < 0) projectile.transform.localScale = new Vector3(-projectile.transform.localScale.x, projectile.transform.localScale.y, projectile.transform.localScale.z);

        // On ajoute une force au Rigidbody2D pour l'envoyer correctement (axe * sens, intensité)
        projectile.GetComponent<Rigidbody2D>().AddForce(originCast.transform.right * Mathf.Sign(-caster.transform.localScale.x) * _speed);
                //Debug.Log(originCast.transform.right);

        // On set le Controller du projectile
        ProjectileController controller = projectile.GetComponent<ProjectileController>();
        controller.ProjectileSpell = this;
        controller.Caster = caster;

        // On appelle une fonction potentiellement override par une classe fille si des modifications doivent être faites sur le projectile
        PostCast(projectile);
    }

    /// <summary>
    /// Fonction appelée par le ProjectileController en cas d'impact avec autre chose que les tags Player et Magic
    /// </summary>
    /// <param name="collision">La collision rapportée par le projectile</param>
    /// <param name="projectile">L'object projectile lui même</param>
    virtual public void Impact(Collider2D collision, GameObject projectile) { }

    /// <summary>
    /// Pour des actions supplémentaires à faire sur le projectile dans la fille (Classe du sort)
    /// </summary>
    /// <param name="projectile">Le projectile créé</param>
    virtual public void PostCast(GameObject projectile) { }
}