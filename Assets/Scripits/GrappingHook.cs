using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappingHook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public bool hooked;

    public GameObject hookedobj;

    public float maxDistance;
    private float currentDistance;

    private void Update()
    {
        //Firing the hook
        if (Input.GetKeyDown(KeyCode.E) && fired == false)
        {
            fired = true;
        }

        //Fired but not hooked
        if (fired == true && hooked == false)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                returnHook();
            }
        }

        //Fired and Hooked
        if (hooked == true && fired == true)
        {
            hook.transform.parent = hookedobj.transform;
            this.GetComponent<PlayerMovement>().velocity.y = 0f;
            this.GetComponent<CharacterController>().enabled = false;
            this.GetComponent<PlayerMovement>().gravity = 1f;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

   
            if (distanceToHook < 2)
            {
                returnHook();
            }
            else
            {
                hook.transform.parent = hookHolder.transform;
                this.GetComponent<CharacterController>().enabled = true;
            }
        }

        if (fired == false)
        {
            hooked = false;
        }

    }

    void returnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.parent = hookHolder.transform;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<PlayerMovement>().gravity = -19f;
    }
}
