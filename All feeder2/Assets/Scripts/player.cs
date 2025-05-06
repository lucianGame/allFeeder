using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;
    public LayerMask pickupObject;

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10); 

        //runs if the player is looking at an object that can be picked up
        if (Physics.Raycast(ray, out hit, 2, pickupObject))
        {
            pickupables.near = true;
        }

        else
        {
            pickupables.near = false;
        }
    }

}
