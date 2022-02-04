using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private ProjectileSpell _projectileSpell;
    public ProjectileSpell ProjectileSpell { get { return _projectileSpell; } set { _projectileSpell = value; } }

    [SerializeField]
    private int test;
    public int Test { get { return test; } set { test = value; } }

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

    /*private void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2);
    }*/
}