using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D rb;

    private PlayerCasting _characterCasting;
    private PlayerController _characterMovment;
    private StatsPlayerManager _characterStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _characterCasting = GetComponent<PlayerCasting>();
        _characterMovment = GetComponent<PlayerController>();
        _characterStats = GetComponent<StatsPlayerManager>();
    }

    private void OnEnable()
    {
        _characterCasting.eventCast += AnimAttack;
        _characterStats.eventDeath += AnimDeath;
        _characterStats.eventDamage += AnimDamage;
    }
    private void OnDisable()
    {
        _characterCasting.eventCast -= AnimAttack;
        _characterStats.eventDeath -= AnimDeath;
        _characterStats.eventDamage += AnimDamage;
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        _animator.SetBool("IsGrounded", _characterMovment.IsGrounded);
    }

    private void AnimAttack(MagicSpell spell)
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

    private void AnimDeath()
    {
        _animator.Play("MageDead");
    }

    private void AnimDamage()
    {
        //Debug.Log("Anim damage");
        //_animator.Play("MageTakeDamage");
    }
}