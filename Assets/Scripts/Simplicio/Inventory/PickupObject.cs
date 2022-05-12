using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool inDebug = false;
    //private bool _islookAt = false;
    private ItemObject _lookAtTarget;
    private Vector3 _TargetPosition;
    public Camera cam;
    [SerializeField] private int distance = 10;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Collectible")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _lookAtTarget = hit.collider.gameObject.GetComponent<ItemObject>();
                    _lookAtTarget.OnHandlePickupItem();
                }
            }
        }
    }
}
