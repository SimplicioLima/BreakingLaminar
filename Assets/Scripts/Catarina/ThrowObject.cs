using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject throwingObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    public void Throw()
    {
        GameObject grenade = Instantiate(throwingObject, transform.position, transform.rotation);
        Rigidbody rb = throwingObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
