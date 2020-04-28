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

    public float maxDistance;
    private float currentDistance;

    private void Update()
    {
        //Firing the hook
        if (Input.GetKeyDown(KeyCode.E) && fired == false)
        {
            fired = true;
        }

        if (fired == true && hooked == false)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                returnHook();
            }
        }

        if (hooked == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            //this.GetComponent<Rigidbody>().useGravity = false;


            if (distanceToHook < 1)
            {
                returnHook();
            }
            else
            {
                //this.GetComponent<Rigidbody>().useGravity = true;
                
            }
        }

    }

    void returnHook()
    {
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }
}
