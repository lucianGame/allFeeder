using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{
    [SerializeField] List<string> lines; //stores lines of dialogue

    public List<string> Lines
    {
        get {return lines;}
    }
}
