using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Hookable" && other.tag != "HookHolder" && other.tag != "Player" && other.tag != "MainCamera")
        {
            print("hit");
            player.GetComponent<GrappingHook>().hooked = false;
            player.GetComponent<GrappingHook>().hitObject = true;
        }
        if (other.tag == "Hookable")
        {
            player.GetComponent<GrappingHook>().hooked = true;
            player.GetComponent<GrappingHook>().hookedobj = other.gameObject;
        }
    }

}
