using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //L'objet à suivre
    private GameObject _player;

    [SerializeField, Tooltip("Latence entre le mouvement de la caméra et du player")]
    private float _timeOffset = 0.2f;

    [SerializeField, Tooltip("Décalage de la caméra vis-à-vis du joueur")]
    private Vector3 _posOffset = new Vector3(1,1,-10);

    private Vector3 velocity;

    private void Start()
    {
        _player = FindObjectOfType<CharacterMovement>().gameObject;
    }

    void Update()
    {
        if(_player != null)
        transform.position = Vector3.SmoothDamp(transform.position, _player.transform.position + _posOffset, ref velocity, _timeOffset);
    }
}