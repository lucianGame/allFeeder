using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{

    public string correctOrder = "0123";
    public static string playerOrder = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if the buttons are pressed in the correct order
        if(playerOrder == "0123")
        {
            Debug.Log("Hooray!");
            button.puzzleSolved = true;
        }

        //if four buttons have been pressed but the order is incorrect
        if(playerOrder.Length == 4 && playerOrder != "0123")
        {
            playerOrder = ""; //reset the order
        }
    }
}
