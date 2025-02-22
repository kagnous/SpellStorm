using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Untagged")
        {
            if(collision.gameObject.tag == "Player")
            {
                LevelManager levelManager = FindObjectOfType<LevelManager>();
                levelManager.GameOver();
            }
            else
            Destroy(collision.gameObject);
        }
    }
}