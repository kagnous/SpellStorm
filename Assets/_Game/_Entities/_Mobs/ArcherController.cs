using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : EnnemiController
{
    [SerializeField, Tooltip("Projectile tirée par le mob")]
    private GameObject _defaultProjectile;

    [SerializeField, Tooltip("Vitesse du projectile")]
    private float _speed;

    [SerializeField, Tooltip("Cadence de tir du mob")]
    private float _recharge;

    private bool _canShoot = true;

    [SerializeField, Tooltip("Ecart de précision des tirs du mob (0 = touche assurée si le player ne bouge pas)")]
    private int _imprecision;

    protected override void Attack()
    {
        if(_canShoot)
        {
            Debug.Log("Attack");
            //On récupère les coordonnées du point de tir
            Transform originShoot = transform.Find("ShootPoint");

            //On instancie le projectile et on le place au point de tir
            GameObject projectile = Instantiate(_defaultProjectile);
            projectile.transform.position = originShoot.position;

            // On assigne que le tireur est le mob pour éviter les collisions, ainsi que les dégâts
            ArrowController controller = projectile.GetComponent<ArrowController>();
            controller.Shooter = gameObject;
            controller.Damage = _damage;

            // On met le projectile dans le bon sens
            if (transform.localScale.x < 0) projectile.transform.localScale = new Vector3(-projectile.transform.localScale.x, projectile.transform.localScale.y, projectile.transform.localScale.z);

            // On ajoute une force au Rigidbody2D pour l'envoyer correctement (direction, intensité)
            Vector2 target = _player.transform.position - transform.position;

            // On crée un point à viser décalé du player pour viser un peu de travers (le + en y est pour compenser la gravité de la flèche)
            Vector2 shoot = new Vector2(target.x + Random.Range(-_imprecision / 2, _imprecision / 2), target.y + Random.Range(-_imprecision / 4, _imprecision / 2) +2);

            projectile.GetComponent<Rigidbody2D>().AddForce(shoot * _speed);

            StartCoroutine(RechargeCoroutine());
        }
    }

    IEnumerator RechargeCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_recharge);
        _canShoot = true;
    }
}