using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{

    [SerializeField] int lettersPerSec;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject dialogueBox;

   // [SerializeField] playerMove PlayerMove;
   // [SerializeField] playerLook PlayerLook;

    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;
    bool isTalking;

    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
       // PlayerLook.enabled = false; //the player cannot move the camera during dialogue
      //  PlayerMove.enabled = false; //the player cannot move during dialogue.
        yield return new WaitForEndOfFrame();
        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[currentLine])); //types out dialogue starting on line 0
    }



    public IEnumerator TypeDialogue(string line) //types dialogue out one line at a time
    {
        isTyping = true;
        dialogueText.text = ""; //start out with a blank dialogue box
        foreach (var letter in line.ToCharArray()) //play the loop once for each letter in the dialogue
        {
            dialogueText.text += letter; //adds one letter to the text box
           // if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
         //   {
              //  yield return new WaitForSeconds(1f / fastLetters); //text goes faster
           // }
          //  else
            {
                yield return new WaitForSeconds(1f / lettersPerSec); //controls how many letters per second appear
            }
            
        }

        yield return new WaitForSeconds(1);
        isTyping = false;

        NextLine();

    }


    public void NextLine() //go to the next line of dialogue
    {
        if (Input.GetKey(KeyCode.E)|| !isTyping) //will only go to next line is the current line is done typing
        {
            ++currentLine; //goes to the next line of dialogue
            if(currentLine < dialogue.Lines.Count) //if there are still lines to be played
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine])); //plays the next line
            }
            else
            {
              //  PlayerMove.enabled = true;
              //  PlayerLook.enabled = true;
                currentLine = 0; //reset the dialogue upon closing it
                dialogueBox.SetActive(false);
                Interactable.talking = false;
            }
        }
    }
}
