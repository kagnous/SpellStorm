using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform _playerSpawn;
    private Animator _animator;

    private void Awake()
    {
        _playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _playerSpawn.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _animator.enabled = true;
            FindObjectOfType<LevelManager>().Saving();
        }
    }
}
