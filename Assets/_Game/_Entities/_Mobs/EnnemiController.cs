using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{
    /// <summary>
    /// Diff�rents �tats possibles d'un mob
    /// </summary>
    public enum EnnemiState
    {
        none,
        patrol,
        search,
        attack,
        freeze
    }

    protected StatsManager _mobStats;
    protected PlayerController _player;
    private Rigidbody2D rb;

    [SerializeField, Tooltip("D�g�ts inflig�s")]
    protected int _damage;

    [SerializeField, Tooltip("Port�e du champ de vision")]
    private float _fieldOfView = 5;

    #region Patrouille
    [SerializeField, Tooltip("Points de passage de la patrouille (si il y a)")]
    private List<Transform> waypoints;
    private int destpoint = 0;
    #endregion

    private Transform target; public Transform Target { get { return target; } set { target = value; } }

    [SerializeField, Tooltip("Etat de l'ennemi par d�faut")]
    protected EnnemiState _defaultState; public EnnemiState DefaultState { get { return _defaultState; } set { _defaultState = value; } }

    protected EnnemiState _state; public EnnemiState State { get { return _state; } set { _state = value; } }

    // Event
    public delegate void AttackDelegate();
    public event AttackDelegate eventAttack;

    private void Awake()
    {
        _mobStats = GetComponent<StatsManager>();
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
            // Si il est en mode patrouille, il se d�place et cherche le Player
            case EnnemiState.patrol:
                Patrol();
                SearchPlayer();
                break;
            // Si reste juste � chercher
            case EnnemiState.search:
                SearchPlayer();
                break;
            // Si freeze, on p�te de suite l'update
            case EnnemiState.freeze:
                return;
            // Si none il reste l� � rien faire
            default:
                break;
        }
        Flip();
    }

    private void Patrol()
    {
        // Se dirige vers son point de ronde
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * _mobStats.Speed * Time.fixedDeltaTime, Space.World);

        // Si il l'a atteint (marge de s�curit� si les points sont pas � la m�me hauteur ou autre connerie
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            // La target devient le point suivant dans la liste
            destpoint = (destpoint + 1) % waypoints.Count;
            target = waypoints[destpoint];
        }
    }

    /// <summary>
    /// Trace un raycast qui d�tecte le joueur et en fait sa target
    /// </summary>
    private void SearchPlayer()
    {
        // On r�cup�re la valeur en int du layer "Player"
        LayerMask mask = LayerMask.GetMask("Player");
        // On trace un raycast de soi m�me jusqu'� la limite du champs de vision vers la droite, en cherchant le layer "PLayer uniquement"
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, _fieldOfView, mask);
        // Si on a trouv� quelque chose (donc fondamentalement le joueur)
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
        eventAttack?.Invoke();
    }

    // En cas de collision...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Avec le Player...
        if (collision.gameObject.tag == "Player")
        {
            if(_state != EnnemiState.freeze)
            {
                // On applique les d�g�ts (rappel : m�me � 0 les d�g�ts sont minimis�s � 1 pour le player)
                collision.gameObject.GetComponent<StatsPlayerManager>().PhysicalDamage(_damage);

                // Il passe � l'attaque
                target = _player.transform;
                _state = EnnemiState.attack;
            }
        }
    }

    /// <summary>
    /// Teste le sens du mob et inverse son scale si besoin pour �tre tourn� vers sa target
    /// </summary>
    private void Flip()
    {
        if(target != null)
        {
            if (transform.position.x - target.transform.position.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else //if (transform.position.x - target.transform.position.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}