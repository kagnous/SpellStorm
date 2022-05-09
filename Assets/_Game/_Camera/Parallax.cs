using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght, startPos;
    [SerializeField]
    private GameObject cam;

    [SerializeField, Tooltip("Valeur du parallax :\nPlus il est élevé plus il suivra la camera")]
    private float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + lenght)
            startPos += lenght;
        else if (temp < startPos - lenght)
            startPos -= lenght;
    }
}