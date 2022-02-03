using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField, Tooltip("player speed")]
    private float _speed; public float Speed => _speed;

    [SerializeField, Tooltip("player jump intensity")]
    private float _jumpForce; public float JumpForce => _jumpForce;

    private Rigidbody2D rb;

    private GameInput _inputsInstance = null;

    private void Awake()
    {
        _inputsInstance = new GameInput();
        rb = GetComponent<Rigidbody2D>();
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

    private void Move(InputAction.CallbackContext context)
    {
        
    }

    private void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(new Vector2(0f, _jumpForce));
    }
}