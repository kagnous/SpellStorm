using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private StatsManager _playerStats;

    private Rigidbody2D rb;

    private Vector3 velocity = Vector3.zero;

    private GameInput _inputsInstance = null;

    #region GroundCheck
    /// <summary>
    /// Booléen de contact avec le sol (true si au sol, false si en l'air)
    /// </summary>
    private bool isGrounded;

    [SerializeField, Tooltip("Point de référence pour la vérification du contact au sol")]
    private Transform groundCheck;

    [SerializeField, Tooltip("Rayon de la sphère de vérification du contact au sol")]
    private float groundCheckRadius;

    [SerializeField, Tooltip("Layer détectés pour la vérification du contact au sol")]
    private LayerMask collisionLayer;
    #endregion

    /// <summary>
    /// Vecteur de la direction du player 
    /// </summary>
    private Vector2 _directionMovment;

    private void Awake()
    {
        _inputsInstance = new GameInput();
        rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<StatsManager>();
    }

    private void OnEnable()
    {
        // Assignation des fonctions aux Inputs
        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Move.performed += Move;
        _inputsInstance.Player.Jump.performed += Jump;
    }
    private void OnDisable()
    {
        // Désassignation des fonctions aux Inputs
        _inputsInstance.Player.Move.performed -= Move;
        _inputsInstance.Player.Jump.performed -= Jump;
    }

    private void FixedUpdate()
    {
        #region Move
        // Calcul de la velocité en fonction du mouvement player sur x, la speed et le temps
        Vector3 targetVelocity = new Vector2(_directionMovment.x * _playerStats.Speed * Time.deltaTime, rb.velocity.y);
        // Mouvement en fonction de la velocité calculée ci-desssus
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        #endregion

        Flip(rb.velocity.x);

    }

    /// <summary>
    /// Settings de _directionMovment selon les Input du player
    /// </summary>
    public void Move(InputAction.CallbackContext context)
    {
        _directionMovment = context.ReadValue<Vector2>();
                //Debug.Log("Move\n"+ context.ReadValue<Vector2>());
    }

    /// <summary>
    /// Application du saut à l'Input du player
    /// </summary>
    private void Jump(InputAction.CallbackContext context)
    {
        // On teste si le player est actuellement en contact avec le sol (du moins une collision de collisionLayer) dans le rayon de groundCheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        // Si oui on applique une force vers le haut en fonction de la JumpForce du player
        if(isGrounded)  rb.AddForce(new Vector2(0f, _playerStats.JumpForce));
    }

    /// <summary>
    /// Scale le player sur x entre positif et négatif pour le tourner selon son mouvement
    /// </summary>
    /// <param name="_velocity">Mouvement sur l'axe x</param>
    private void Flip(float _velocity)
    {
        // Si son mouvement est positif...
        if(_velocity > 0.1f)
        {
            // Son scale sur x vaut -1
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        // Sinon s'il est négatif...
        else if (_velocity < -0.1f)
        {
            // Son scale sur x vaut 1
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    /// Pour les gizmos (zone de groundCheck)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}