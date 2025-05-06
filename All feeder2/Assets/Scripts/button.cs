using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject platform;
    public GameObject bigButton;

    private Animator anim;
    private Animator buttonAnim;

    private bool platformIsMoving;

    [SerializeField] private Waypoints waypointPath;
    [SerializeField] private float speed;
    private int targetWaypointIndex;

    private Transform previousWaypoint;
    private Transform targetWaypoint;

    private float timeToWaypoint;
    private float elapsedTime;

    private bool check;

    private FMOD.Studio.EventInstance PlatformSound;
    private FMOD.Studio.EventInstance NoteSound;

    public int note;
    static public bool puzzleSolved;

    private void Start()
    {
        buttonAnim = bigButton.GetComponent<Animator>();

        platformIsMoving = false;

        check = false;

        TargetNextWaypoint(); //sets the waypoint targets
    }

    void Update()
    {
        if(puzzleSolved)
        {
            MovePlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        buttonAnim.SetTrigger("down");

        if (gameObject.CompareTag("music_button")){
            PlayNote(note); //play the correct note according to the button pressed
            ButtonCode.playerOrder += note; //add the note pressed to the player code input
            Debug.Log(ButtonCode.playerOrder);
        }

        if (gameObject.CompareTag("platform_button")){
            PlatformSound = FMODUnity.RuntimeManager.CreateInstance("event:/Platform");
            PlatformSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(platform.transform.position));
            PlatformSound.start();
            PlatformSound.release();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.CompareTag("platform_button")){
            MovePlatform();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        buttonAnim.SetTrigger("up");

        if (gameObject.CompareTag("platform_button")){
            platformIsMoving = false;
        }
    }

     void MovePlatform()
    {
        //move the platform connected to the button
        elapsedTime += Time.deltaTime;

            float elapsedPercentage = elapsedTime / timeToWaypoint;
            platform.transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
            
            if(elapsedPercentage >=1)
            {
                TargetNextWaypoint();
            }
    }

    private void TargetNextWaypoint()
    {
        previousWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / speed;
    }

    void PlayNote(int note)
    {
        //select and play the correct note for the button
         NoteSound =FMODUnity.RuntimeManager.CreateInstance("event:/Music Button");
         NoteSound.setParameterByName("Note", note);
         NoteSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
         NoteSound.start();
         NoteSound.release();
    }
}
