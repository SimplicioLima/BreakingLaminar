using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool inDebug = true;
    private bool _islookAt = false;
    private ItemObject _lookAtTarget;
    private Vector3 _TargetPosition;
    public Camera cam;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_islookAt)
            {
                _lookAtTarget.OnHandlePickupItem();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Collectible")
        {
            _lookAtTarget = other.gameObject.GetComponent<ItemObject>();
            _islookAt = true;
            if (inDebug) Debug.Log("Collides with " + other.name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _islookAt = false;
        _lookAtTarget = null;
    }

}
