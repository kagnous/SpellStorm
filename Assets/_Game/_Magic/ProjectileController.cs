using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField, Tooltip("Spell � l'origine du projectile")]
    private ProjectileSpell _projectileSpell;
    public ProjectileSpell ProjectileSpell { get { return _projectileSpell; } set { _projectileSpell = value; } }

    /// <summary>
    /// Entit� qui a incant� le spell
    /// </summary>
    private GameObject _caster;
    public GameObject Caster { get { return _caster; } set { _caster = value; } }

    /// <summary>
    /// Point d'origine du projectile
    /// </summary>
    private Vector3 origin;

    private void Start()
    {
        // On r�cup�re la valeur d'origin avant de se mettre � avancer
        origin = _caster.transform.position;
    }

    private void FixedUpdate()
    {
        // Si la distance entre le projectile et son point d'origine est sup�rieur � sa port�e...
        if((transform.position - origin).magnitude > _projectileSpell.Range)
        {
            // Le projectile est d�truit
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la collision n'est ni avec Player ou Magic...
        if (collision.gameObject.tag != "Magic" && collision.gameObject != _caster)
        {
            // On appelle la fonction Impact du spell correspondant (si il est assign�)
            if (_projectileSpell != null)   _projectileSpell.Impact(collision, gameObject);

            // Puis on d�truit le projectile
            Destroy(gameObject);
        }
    }

    // Pour visualiser la range de l'effet FireBall
    /*private void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2);
    }*/
}