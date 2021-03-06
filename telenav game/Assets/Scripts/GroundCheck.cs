using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool player = true;

    private void OnTriggerStay(Collider other)
    {
        if (player)
        {
            PlayerController.instance.grounded2 = true;
        }
        if (other.transform.name.ToLower().Contains("perna") || other.transform.parent != null && other.transform.parent.name.ToLower().Contains("perna"))
        {
            PlayerController.instance.onPillow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player)
        {
            PlayerController.instance.grounded2 = false;
        }
        if (other.transform.name.ToLower().Contains("perna") || other.transform.parent != null && other.transform.parent.name.ToLower().Contains("perna"))
        {
            PlayerController.instance.onPillow = false;
        }
    }
}
