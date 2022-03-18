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
    }
    private void OnDisable()
    {
        _stats.eventDamage -= AnimDamage;
        _controller.eventAttack -= AnimAttack;
    }

    private void Update()
    {
        if(_controller.State == EnnemiController.EnnemiState.attack || _controller.State == EnnemiController.EnnemiState.patrol)
        {
            _animator.SetBool("Move", true);
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
}