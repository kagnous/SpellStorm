using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnnemiManager : StatsManager
{
    private EnnemiController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnnemiController>();
    }

    protected override void SetLife(int modifLife)
    {
        //controller.Target = controller.Player.transform;
        //controller.State = EnnemiController.EnnemiState.Attack;
        base.SetLife(modifLife);
    }

    protected override void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnnemiController>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().Sleep();
    }
}