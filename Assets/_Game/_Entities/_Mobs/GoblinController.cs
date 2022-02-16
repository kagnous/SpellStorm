using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    private StatsManager _goblinStats;
    private CharacterMovement _player;
    private Rigidbody2D rb;

    [SerializeField]
    private List<Transform> waypoints;
    private Transform target;
    //private int destpoint = 0;

    private bool _isFreeze; public bool IsFreeze { get { return _isFreeze; } set { _isFreeze = value; } }

    private void Awake()
    {
        _goblinStats = GetComponent<StatsManager>();
        _player = FindObjectOfType<CharacterMovement>();
        rb = GetComponent<Rigidbody2D>();
        target = waypoints[0];
    }

    /*private void FixedUpdate()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * _goblinStats.Speed * Time.fixedDeltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destpoint = (destpoint + 1) % waypoints.Count;
            target = waypoints[destpoint];
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!_isFreeze)
            collision.gameObject.GetComponent<StatsManager>().PhysicalDamage(3);
        }
    }
}