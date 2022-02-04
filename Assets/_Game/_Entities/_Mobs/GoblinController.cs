using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    private StatsManager _goblinStats;

    //private Vector2 _directionMovment;
    private CharacterMovement _player;

    private Rigidbody2D _rb;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        _goblinStats = GetComponent<StatsManager>();
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<CharacterMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<StatsManager>().SetLife(-3);
        }
    }
}