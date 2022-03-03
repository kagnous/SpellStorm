using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    private StatsManager _goblinStats;
    private PlayerController _player;
    private Rigidbody2D rb;

    [SerializeField, Tooltip("Dégâts infligés")]
    private int _damage;

    [SerializeField, Tooltip("Portée du champ de vision")]
    private float _fieldOfView = 5;

    #region Patrouille
    [SerializeField, Tooltip("Si le mob effectue une patrouille")]
    private bool _isPatrol = false;

    [SerializeField, Tooltip("Points de passage de la patrouille (si il y a)")]
    private List<Transform> waypoints;
    private Transform target;
    private int destpoint = 0;
    #endregion

    // Si il est en mode attaque
    private bool _isAttacking = false; public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

    // Si il est sous l'effet de paralysie (typiquement flèche de glace)
    private bool _isFreeze; public bool IsFreeze { get { return _isFreeze; } set { _isFreeze = value; } }

    private void Awake()
    {
        _goblinStats = GetComponent<StatsManager>();
        _player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        
        // On assigne les waypoints de patrouille que si y en a
        if(waypoints.Count > 0) target = waypoints[0];
    }

    private void FixedUpdate()
    {
        // Si freezé, on pète de suite l'update
        if(_isFreeze)
        {
            return;
        }

        // Si en mode attaque...
        if(_isAttacking)
        {
            // Si il est à proximité du player
            if (Vector3.Distance(transform.position, target.position) < 1.5)
            {
                // Il attaque
                //Debug.Log("attack");
            }
            // Sinon...
            else
            {
                // Se dirige vers le player
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * _goblinStats.Speed * Time.fixedDeltaTime, Space.World);
            }
        }
        // Sinon si il est en mode patrouille
        else if (_isPatrol)
        {
            // Se dirige vers son point de ronde
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * _goblinStats.Speed * Time.fixedDeltaTime, Space.World);

            // Si il l'a atteint (marge de sécurité si les points sont pas à la même hauteur ou autre connerie
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                // La target devient le point suivant dans la liste
                destpoint = (destpoint + 1) % waypoints.Count;
                target = waypoints[destpoint];
            }

            // Cherche le joueur
            SearchPlayer();
        }
        // Sinon (donc n'a aucun mode particulier actif)
        else
        {
            // Cherche le joueur
            SearchPlayer();
        }
    }

    /// <summary>
    /// Trace un raycast qui détecte le joueur et en fait sa target
    /// </summary>
    private void SearchPlayer()
    {
        // On récupère la valeur en int du layer "Player"
        LayerMask mask = LayerMask.GetMask("Player");
        // On trace un raycast de soi même jusqu'à la limite du champs de vision vers la droite, en cherchant le layer "PLayer uniquement"
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right, _fieldOfView, mask);
        // Si on a trouvé quelque chose (donc fondamentalement le joueur)
        if (raycast.collider != null)
        {
            Debug.Log("VU !");
            // On fait de sa transform la nouvelle cible
            target = raycast.collider.transform;
            // On passe en mode attaque
            _isAttacking = true;
            _isPatrol = false;
        }
    }

    // En cas de collision...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Avec le Player...
        if (collision.gameObject.tag == "Player")
        {
            // On applique les dégâts (rappel : même à 0 les dégâts sont minimisés à 1 pour le player)
            collision.gameObject.GetComponent<StatsPlayerManager>().PhysicalDamage(_damage);
        }
    }
}