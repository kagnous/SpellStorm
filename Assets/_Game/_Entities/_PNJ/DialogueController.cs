using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : Interractible
{
    [SerializeField, Tooltip("Le dialogue du PNJ")]
    private Dialogue _dialogue;

    private Animator animator;
    private Transform player;
    private SpriteRenderer sprite;

    private bool _isTalking = false; public bool IsTalking { get { return _isTalking; } set { _isTalking = value; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>().transform;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Interract()
    {
        if(!_isTalking)
        DialogueManager.instance.StartDialogue(_dialogue, this);

        _isTalking = true;
    }

    private void Update()
    {
        animator.SetBool("IsTalking", _isTalking);
        if(player.position.x > transform.position.x)
        {
            sprite.flipX = true;
        }
        else
            sprite.flipX = false;
    }
}