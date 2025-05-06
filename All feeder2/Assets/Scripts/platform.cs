using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public GameObject player;
    public Transform movingPlatform;

    public static bool isMoving;


    private void OnTriggerEnter(Collider other)
    {
        player.transform.SetParent(movingPlatform);
    }

    private void OnTriggerExit(Collider other)
    {
        player.transform.SetParent(null);
    }

    
}
