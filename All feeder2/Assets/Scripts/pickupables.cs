using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupables : MonoBehaviour
{
    public GameObject cube;
    public Camera mainCamera;


    float distance = 0.7f;

    public static bool near;
    public bool holding;

    public Rigidbody rb;

    private FMOD.Studio.EventInstance PickupSound;

    private void Start()
    {
        rb = cube.GetComponent<Rigidbody>();
        holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        near = true;
        //runs if the player presses E while close an object and is not holding an object
        if (Input.GetKeyDown(KeyCode.E) && near && !holding)
        {
            rb.isKinematic = true;
            //the object will follow the camera
            cube.transform.SetParent(mainCamera.transform);
            //the object is position in front of the camera
            cube.transform.position = mainCamera.transform.position + mainCamera.transform.forward  * distance;
            holding = true;

            PickupSound = FMODUnity.RuntimeManager.CreateInstance("event:/Pickup");
            PickupSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PickupSound.start();
            PickupSound.release();

        }

        //runs if the player presses E while holding the object
        else if (Input.GetKeyDown(KeyCode.E) && holding)
        {
            rb.isKinematic = false;
            this.cube.transform.SetParent(null);
            holding = false;

        }

        else
        {
           // rb.isKinematic = false;
          //  cube.transform.SetParent(null);
        }
    }
}
