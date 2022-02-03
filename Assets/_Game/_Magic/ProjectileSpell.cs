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

    private void Awake()
    {
        // ATTENTION, NE CE FAIT QUE LORS DE LA CREATION DU SCRIPTABLE (Trouver un meilleurs moyen d'être sûr sans à faire le if ci-dessous à chaque Cast ?)
        Debug.Log("Awake");
        originCast = FindObjectOfType<CharacterMovement>().transform.Find("CastSpawn");
    }

    public override void Cast()
    {
        if(originCast == null) originCast = FindObjectOfType<CharacterMovement>().transform.Find("CastSpawn");

        GameObject projectile = Instantiate(defaultProjectile);
        projectile.transform.position = originCast.position;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(originCast.transform.right * -1 * _speed);

        // EN COURS DE DEV
        //projectile.GetComponent<ProjectileController>().ProjectileSpell = { LUI MEME };
    }

    virtual public void Impact()
    {
        Debug.Log("Impact");
    }
}