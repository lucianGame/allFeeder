using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour

{
    private enum CURRENT_TERRAIN { GROUND, STOMACH, NONE };

    private Rigidbody rb;

    float timer = 0.0f;

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;
    private FMOD.Studio.EventInstance Footsteps;

    public float footstepSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DetermineTerrain();
        TimeSteps();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            footstepSpeed = 0.4f;
        }
        else
        {
            footstepSpeed = 0.5f;
        }
    }

    void DetermineTerrain()
    {
        //a raycast is pointed down and intersects with the ground
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, Vector3.down, 1.0f);

        //the raycast finds the name of the layer it is hitting and assigns a terrain
        foreach (RaycastHit raycastHit in hit)
        {
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = CURRENT_TERRAIN.GROUND;
                break;
            }
            else if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Gravel"))
            {
                currentTerrain = CURRENT_TERRAIN.STOMACH;
                break;
            }
            else if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                currentTerrain = CURRENT_TERRAIN.NONE;
                break;
            }
        }

    }

    void TimeSteps()
    {
        if (rb.velocity.magnitude > 1f && Mathf.Abs(rb.velocity.y) < 0.1f) //if the player is moving and grounded
        {
            if (timer > footstepSpeed)
            {
                SelectAndPlayFootsteps();
                //PlayFootsteps();
                timer = 0.0f;
            }

            timer += Time.deltaTime;
        }
    }

    public void SelectAndPlayFootsteps()
    {
        //Assigns an integer to each terrain and runs it through our PlayFootsteps function
        //These integers are also assigned in FMOD studio
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GROUND:
                PlayFootsteps(0);
                break;
            case CURRENT_TERRAIN.STOMACH:
                PlayFootsteps(1);
                break;
            default:
                PlayFootsteps(1);
                break;
        }
    }

    void PlayFootsteps(int terrain)
    {
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        Footsteps.setParameterByName("Terrain", terrain);
        Footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Footsteps.start();
        Footsteps.release();
    }

}
