using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private GameObject _shooter; public GameObject Shooter { get { return _shooter; } set { _shooter = value; } }

    private int _damage; public int Damage { get { return _damage; } set { _damage = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la collision n'est ni avec le lanceur ou un object interractible...
        if (collision.gameObject != _shooter && collision.gameObject.tag != "Interractible")
        {
            StatsManager stats;
            if(collision.gameObject.TryGetComponent<StatsManager>(out stats))
            {
                stats.PhysicalDamage(_damage);
            }

            //Debug.Log(collision.name);
            // On détruit le projectile
            Destroy(gameObject);
        }
    }
}
