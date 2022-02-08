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

    [SerializeField, Tooltip("Prefab de projectile magique")]
    protected GameObject defaultProjectile;

    public override void Cast(MagicSpell spell, GameObject caster)
    {
                    // Syst�me antispam (pas finis...)
                    //CharacterCasting characterCasting = caster.GetComponent<CharacterCasting>();
                    //if (characterCasting.CanCast)
        {
            //On r�cup�re les coordonn�es d'invocation du sort
            Transform originCast = caster.transform.Find("CastSpawn");

            //On instancie le projectile et on le place au point d'invocation
            GameObject projectile = Instantiate(defaultProjectile);
            projectile.transform.position = originCast.position;

            // On r�cup�re son sprite, on le set on le met dans le bon sens
            if (spellSprite != null) projectile.GetComponent<SpriteRenderer>().sprite = spellSprite;
            if(caster.transform.localScale.x < 0)   projectile.transform.localScale = new Vector3(-projectile.transform.localScale.x, projectile.transform.localScale.y, projectile.transform.localScale.z);
           
            // On ajoute une force au Rigidbody2D pour l'envoyer correctement (axe, sens, intensit�)
            projectile.GetComponent<Rigidbody2D>().AddForce(originCast.transform.right * -caster.transform.localScale.x * _speed);

            // On set le Controller du projectile
            ProjectileController controller = projectile.GetComponent<ProjectileController>();
            controller.ProjectileSpell = (ProjectileSpell)spell;
            controller.Caster = caster;

            // On appelle une fonction potentiellement override par une classe fille si des modifications doivent �tre faites sur le projectile
            PostCast(projectile);

                        //characterCasting.CanCast = false;
        }
    }

    /// <summary>
    /// Fonction appel�e par le ProjectileController en cas d'impact avec autre chose que les tags Player et Magic
    /// </summary>
    /// <param name="collision">La collision rapport�e par le projectile</param>
    /// <param name="projectile">L'object projectile lui m�me</param>
    virtual public void Impact(Collider2D collision, GameObject projectile) { }

    /// <summary>
    /// Pour des actions suppl�mentaires � faire sur le projectile dans la fille (Classe du sort)
    /// </summary>
    /// <param name="projectile">Le projectile cr��</param>
    virtual public void PostCast(GameObject projectile) { }

    public override void EndSpell(MagicSpell projectileSpell, GameObject caster)
    {
                    // Antispam
                    //caster.GetComponent<CharacterCasting>().CanCast = true;
                    //Debug.Log("Tir ok");
    }
}