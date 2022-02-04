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
    private bool isGrounded;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private LayerMask collisionLayer;
    #endregion

    private Vector2 _directionMovment;

    private void Awake()
    {
        _inputsInstance = new GameInput();
        rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<StatsManager>();
    }

    private void OnEnable()
    {
        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Move.performed += Move;
        _inputsInstance.Player.Jump.performed += Jump;
    }
    private void OnDisable()
    {
        _inputsInstance.Player.Move.performed -= Move;
        _inputsInstance.Player.Jump.performed -= Jump;
    }

    private void FixedUpdate()
    {
        #region Move
        Vector3 targetVelocity = new Vector2(_directionMovment.x * _playerStats.Speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        #endregion
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Debug.Log("Move\n"+ context.ReadValue<Vector2>());
        _directionMovment = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        if(isGrounded)
        rb.AddForce(new Vector2(0f, _playerStats.JumpForce));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}