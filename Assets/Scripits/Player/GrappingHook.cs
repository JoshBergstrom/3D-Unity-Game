using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappingHook : MonoBehaviour
{
    //Creats the objects
    public GameObject hook;
    public GameObject hookHolder;
    public GameObject hookedobj;
    //floats 
    public float hookTravelSpeed;
    public float playerTravelSpeed;
    public float maxDistance;
    private float currentDistance;
    //bools
    public static bool fired;
    public bool hooked;
    public bool hitObject;


    private void Update()
    {
        //Firing the hook
        if (Input.GetKeyDown(KeyCode.E) && fired == false)
        {
            fired = true;
        }

        //Rope rendering
        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }
        

        //Fired but not hooked
        if (fired == true && hooked == false)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);
            if (hitObject)
            {
                returnHook();
            }
            if (currentDistance >= maxDistance)
            {
                returnHook();
            }
        }

        //Fired and Hooked
        if (hooked == true && fired == true)
        {
            hook.transform.parent = hookedobj.transform;
            //for when not grounded 
            this.GetComponent<PlayerMovement>().velocity.y = 0f;
            //to move player
            this.GetComponent<CharacterController>().enabled = false;
            this.GetComponent<PlayerMovement>().gravity = 1f;
            this.GetComponent<PlayerMovement>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

   
            if (distanceToHook < 2)
            {
                returnHook();
            }
            else
            {
                //This is just in case the player breaks something
                //Allows player to move again
                hook.transform.parent = hookHolder.transform;
                this.GetComponent<CharacterController>().enabled = true;
                this.GetComponent<PlayerMovement>().enabled = true;
            }
        }

        if (fired == false)
        {
            hooked = false;
        }

    }

    void returnHook()
    {
        //resets hook
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.parent = hookHolder.transform;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
        hitObject = false;
        //Allows player to move again
        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<PlayerMovement>().gravity = -19f;
        this.GetComponent<PlayerMovement>().enabled = true;
        //Stops rendering rope
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
    }
}
