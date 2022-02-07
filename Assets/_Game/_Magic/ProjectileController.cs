using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private ProjectileSpell _projectileSpell;
    public ProjectileSpell ProjectileSpell { get { return _projectileSpell; } set { _projectileSpell = value; } }

    private GameObject _caster;
    public GameObject Caster { get { return _caster; } set { _caster = value; } }

    private Vector3 origin;

    private void Start()
    {
        origin = _caster.transform.position;
    }

    private void FixedUpdate()
    {
        if((transform.position - origin).magnitude > _projectileSpell.Range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Magic")
        {
            if (_projectileSpell != null)
            {
                _projectileSpell.Impact(collision, gameObject);
            }
        Destroy(gameObject);
        }
    }

    // Pour la range de l'effet FireBall
    /*private void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2);
    }*/
}