using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmobController : EnnemiController
{
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

    public void ApplyDamage()
    {
        Debug.Log("attack");
    }
}