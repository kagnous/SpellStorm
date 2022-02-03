using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private ProjectileSpell _projectileSpell;
    public ProjectileSpell ProjectileSpell { get { return _projectileSpell; } set { value = _projectileSpell; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Magic")
        Destroy(gameObject);
    }
}