using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{

    public NPCDialogue dialogueBox;
    [SerializeField] Dialogue dialogue;
    
    public static bool talking = false;

    FMOD.Studio.EventInstance talkSound;

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E) && !talking)
        {
            talking = true;
            Interact();
        }
    }

    void Interact()
    {
        StartCoroutine(dialogueBox.ShowDialogue(dialogue));

        talkSound = FMODUnity.RuntimeManager.CreateInstance("event:/Talk");
        talkSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        talkSound.start();
        talkSound.release();
    }

}
