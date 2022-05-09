using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiAnimator : MonoBehaviour
{
    private Animator _animator;
    private EnnemiController _controller;
    private StatsManager _stats;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<EnnemiController>();
        _stats = GetComponent<StatsManager>();
    }

    private void OnEnable()
    {
        _stats.eventDamage += AnimDamage;
        _controller.eventAttack += AnimAttack;
        _stats.eventDeath += AnimDeath;
    }
    private void OnDisable()
    {
        _stats.eventDamage -= AnimDamage;
        _controller.eventAttack -= AnimAttack;
        _stats.eventDeath -= AnimDeath;
    }

    private void Update()
    {
        if(_controller is SwordmobController)
        {
            if (_controller.State == EnnemiController.EnnemiState.Attack || _controller.State == EnnemiController.EnnemiState.Patrol)
            {
                _animator.SetBool("Move", true);
            }
            else
            {
                _animator.SetBool("Move", false);
            }
        }
        else
        {
            _animator.SetBool("Move", false);
        }
    }

    private void AnimDamage()
    {
        _animator.Play("SwordmanTakeDamage");
    }

    private void AnimAttack()
    {
        _animator.Play("SwordmanAttack");
    }

    private void AnimDeath()
    {
        _animator.Play("SwordmanDead");
    }
}