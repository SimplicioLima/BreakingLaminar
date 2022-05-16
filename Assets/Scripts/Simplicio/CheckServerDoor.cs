using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckServerDoor : MonoBehaviour
{
    public bool UnlockTrigger = false;
    public bool doorFound = false;

    private void OnTriggerEnter(Collider other)
    {
        if (UnlockTrigger) doorFound = true;
    }

}
