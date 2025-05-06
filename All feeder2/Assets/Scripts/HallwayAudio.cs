using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayAudio : MonoBehaviour
{
    public Transform player;
    public Transform hallwaySound;
    public Transform room2Sound;
    public Transform room3Sound;

    private float scaledHallwayDistance;
    private float maxHallwayDistance = 9;

    private float scaledRoom2Distance;
    private float maxRoom2Distance = 10;

    private float scaledRoom3Distance;
    private float maxRoom3Distance = 10;
    
    public static bool inRoom2;
    public static bool inRoom3;

    private FMOD.Studio.EventInstance song;


    void Start()
    {
        song = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Song1");
        song.start();

        inRoom2 = false;
        inRoom3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromHallway = Vector3.Distance(player.position, hallwaySound.position);
        float distanceFromRoom2 = Vector3.Distance(player.position, room2Sound.position);
        float distanceFromRoom3 = Vector3.Distance(player.position, room3Sound.position);
       
        if (distanceFromHallway < maxHallwayDistance)
        {
            scaledHallwayDistance = 1 - (distanceFromHallway/maxHallwayDistance);
            song.setParameterByName("Hallway", scaledHallwayDistance);
        }

       
        if(inRoom2)
        {
            song.setParameterByName("Room 2", 1);
        }
        else if (distanceFromRoom2 < maxRoom2Distance)
        {
        
            scaledRoom2Distance = 1 - (distanceFromRoom2/maxRoom2Distance);
            song.setParameterByName("Room 2", scaledRoom2Distance);
        }

        if(inRoom3)
        {
            song.setParameterByName("Room 3", 1);
        }
        else if (distanceFromRoom3 < maxRoom3Distance)
        {
        
            scaledRoom3Distance = 1 - (distanceFromRoom3/maxRoom3Distance);
            song.setParameterByName("Room 3", scaledRoom3Distance);
        }
        
    }

    void HallwayChange()
    {
        
    }
}
