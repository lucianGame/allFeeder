using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Button : MonoBehaviour
{

    private enum NOTE {E, D, C, Bb, NONE};
    public Rigidbody rb;

    [SerializeField]
    private NOTE note;
    private FMOD.Studio.EventInstance Button;


   void DetermineNote()
    {
        //a raycast is pointed down and intersects with the ground
        RaycastHit[] hit;
        hit = Physics.RaycastAll(rb.transform.position, Vector3.down, 1.0f);

        //the raycast finds the name of the layer it is hitting and assigns a terrain
        foreach (RaycastHit raycastHit in hit)
        {
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("E"))
            {
                note = NOTE.E;
                PlayNote(0);
            }
            else if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("D"))
            {
                note = NOTE.D;
                PlayNote(1);
            }
            else if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("C"))
            {
                note = NOTE.C;
                PlayNote(2);
            }
            else if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Bb"))
            {
                note = NOTE.Bb;
                PlayNote(3);
            }
            
        }

    }

    public void SelectAndPlayNote()
    {
        //Assigns an integer to each terrain and runs it through our PlayFootsteps function
        //These integers are also assigned in FMOD studio
        switch (note)
        {
            case NOTE.E:
                PlayNote(0);
                break;
            case NOTE.D:
                PlayNote(1);
                break;
            case NOTE.C:
                PlayNote(2);
                break;
            case NOTE.Bb:
                PlayNote(3);
                break;
            
        }
    }

    void PlayNote(int note)
    {
        Button = FMODUnity.RuntimeManager.CreateInstance("event:/Music Button");
        Button.setParameterByName("Note", note);
        Button.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Button.start();
        Button.release();
    }
}
