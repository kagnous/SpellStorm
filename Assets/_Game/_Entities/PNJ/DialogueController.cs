using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : Interractible
{
    [SerializeField, Tooltip("Le dialogue du PNJ")]
    private Dialogue _dialogue;

    private bool _isTalking = false; public bool IsTalking { get { return _isTalking; } set { _isTalking = value; } }

    protected override void Interract()
    {
        if(!_isTalking)
        DialogueManager.instance.StartDialogue(_dialogue, this);

        _isTalking = true;
    }
}