using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [Header("Trow Obj")]
    public Transform player;
    public Transform cam;
    public float throwforce = 20.0f;
    bool hasplayer = false;
    bool beingCarried = false;
    public int dag;
    private bool touched = false;

    [Space]
    [Header("Sound")]
    public AudioClip[] soundToPlay;
    //public AudioSource audio;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.gameObject.transform.position, player.position);

        //O Player está no range para pegar no obj
        if (dist < 6f)
        {
            //Debug.Log("Distancia = " + dist);
            hasplayer = true;
        }
        else
        {
            hasplayer = false;
        }

        if (hasplayer && Input.GetKeyDown(KeyCode.Mouse1))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = cam;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (Input.GetMouseButtonUp(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(cam.forward * throwforce, ForceMode.Impulse);
                
                //sound
            }
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (beingCarried)
        {
            
            touched = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!beingCarried)
        {
            touched = false;
        }
    }
    /*
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
    */
}
