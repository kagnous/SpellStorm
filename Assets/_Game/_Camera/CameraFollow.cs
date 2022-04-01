using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //L'objet � suivre
    private GameObject _player;

    [SerializeField, Tooltip("Latence entre le mouvement de la cam�ra et du player")]
    private float _timeOffset = 0.2f;

    [SerializeField, Tooltip("D�calage de la cam�ra vis-�-vis du joueur")]
    private Vector3 _posOffset = new Vector3(1,1,-10); public Vector3 PosOffset => _posOffset;

    private Vector3 velocity;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        transform.position = _player.transform.position + _posOffset;
    }

    void Update()
    {
        // Si le player existe : on fait un mouvement (SmoothDamp) de la position actuelle vers sa position + le d�calage, � sa v�locit�, avec un d�calage de temps donn�
        if(_player != null)
        transform.position = Vector3.SmoothDamp(transform.position, _player.transform.position + _posOffset, ref velocity, _timeOffset);
    }
}