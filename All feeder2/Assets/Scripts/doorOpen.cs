using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    public GameObject door;

    private Animator anim;

    private FMOD.Studio.EventInstance openSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("open");

        //sfx stuff
        openSound = FMODUnity.RuntimeManager.CreateInstance("event:/Door");
        openSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        openSound.start();
        openSound.release();
    }

    private void OnTriggerExit(Collider other)
    {
       // anim.SetTrigger("close");
    }
}
