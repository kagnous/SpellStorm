using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = transform.position + camera.GetComponent<CameraFollow>().PosOffset;
    }
}