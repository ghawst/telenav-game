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
    }

    private void OnTriggerExit(Collider other)
    {
        if (player)
        {
            PlayerController.instance.grounded2 = false;
        }
    }
}
