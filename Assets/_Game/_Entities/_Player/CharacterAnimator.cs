using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D rb;

    private PlayerCasting _characterCasting;
    private PlayerController _characterMovment;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _characterCasting = GetComponent<PlayerCasting>();
        _characterMovment = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _characterCasting.eventCast += Attack;
    }

    private void OnDisable()
    {
        _characterCasting.eventCast -= Attack;
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        _animator.SetBool("IsGrounded", _characterMovment.IsGrounded);
    }

    private void Attack(MagicSpell spell)
    {
        if(Mathf.Abs(rb.velocity.x) > 0.3f)
        {
            _animator.Play("MageWalkingAttack");
        }
        else
        {
            _animator.Play("MageStaticAttack");
        }
    }
}