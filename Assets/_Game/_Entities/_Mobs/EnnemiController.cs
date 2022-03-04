using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{
    /// <summary>
    /// Différents états possibles d'un mob
    /// </summary>
    public enum EnnemiState
    {
        none,
        patrol,
        search,
        attack,
        freeze
    }

    protected StatsManager _goblinStats;
    protected PlayerController _player;
    private Rigidbody2D rb;

    [SerializeField, Tooltip("Dégâts infligés")]
    protected int _damage;

    [SerializeField, Tooltip("Portée du champ de vision")]
    private float _fieldOfView = 5;

    #region Patrouille
    [SerializeField, Tooltip("Points de passage de la patrouille (si il y a)")]
    private List<Transform> waypoints;
    private Transform target;
    private int destpoint = 0;
    #endregion

    [SerializeField, Tooltip("Etat de l'ennemi par défaut")]
    protected EnnemiState _defaultState; public EnnemiState DefaultState { get { return _defaultState; } set { _defaultState = value; } }

    protected EnnemiState _state; public EnnemiState State { get { return _state; } set { _state = value; } }

    private void Awake()
    {
        _goblinStats = GetComponent<StatsManager>();
        _player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        _state = _defaultState;

        // On assigne les waypoints de patrouille que si y en a
        if(waypoints.Count > 0) target = waypoints[0];
    }

    private void FixedUpdate()
    {
        switch(_state)
        {
            // Si en mode attaque, il attaque
            case EnnemiState.attack:
                Attack();
                break;
            // Si il est en mode patrouille, il se déplace et cherche le Player
            case EnnemiState.patrol:
                Patrol();
                SearchPlayer();
                break;
            // Si reste juste à chercher
            case EnnemiState.search:
                SearchPlayer();
                break;
            // Si freeze, on pète de suite l'update
            case EnnemiState.freeze:
                return;
            // Si none il reste là à rien faire
            default:
                break;
        }
    }

    private void Patrol()
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
            _state = EnnemiState.attack;
        }
    }

    /// <summary>
    /// Attaque du mob
    /// </summary>
    protected virtual void Attack()
    {

    }

    // En cas de collision...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Avec le Player...
        if (collision.gameObject.tag == "Player")
        {
            if(_state != EnnemiState.freeze)
            {
                // On applique les dégâts (rappel : même à 0 les dégâts sont minimisés à 1 pour le player)
                collision.gameObject.GetComponent<StatsPlayerManager>().PhysicalDamage(_damage);

                // Il passe à l'attaque
                _state = EnnemiState.attack;
            }
        }
    }
}