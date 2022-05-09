using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmobController : EnnemiController
{
    [SerializeField, Tooltip("Layer détectés pour la vérification du dégât")]
    private LayerMask collisionLayer;

    [SerializeField, Tooltip("Range de dégâts")]
    private Vector2 _range = new Vector2(1.5f, 1);

    protected override void Attack()
    {
        // Si il est à proximité du player
        if (Vector3.Distance(transform.position, _player.transform.position) < 1.5)
        {
            // Il attaque
            base.Attack();
            
        }
        // Sinon...
        else
        {
            // Se dirige vers le player
            Vector3 dir = _player.transform.position - transform.position;
            transform.Translate(dir.normalized * _mobStats.Speed * Time.fixedDeltaTime, Space.World);
        }
    }

    private void ApplyDamage()
    {
            //Debug.Log("attack");
        if(Physics2D.OverlapBox(transform.position + new Vector3(1,0, 0) * transform.localScale.x, _range, 0, collisionLayer))
        {
                //Debug.Log("Damage");
            _player.GetComponent<StatsPlayerManager>().PhysicalDamage(_damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(transform.position + new Vector3(1, 0, 0) * transform.localScale.x, _range);
    }
}