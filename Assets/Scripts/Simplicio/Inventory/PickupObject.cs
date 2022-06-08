using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool inDebug = false;
    //public Material normalMaterial;
    //public Material outlineMaskMaterial;

    private AudioSource m_MyAudioSource;
    private bool _playSound;
    //public GameObject hitObject;
    //private bool _islookAt = false;
    private ItemObject _lookAtTarget;
    public Camera cam;
    [SerializeField] private int distance = 10;

    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        _playSound = false;
    }

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
                    _playSound = true;
                    SoundOn();

                    _lookAtTarget.OnHandlePickupItem();
                }
            }

        }


    }

    

    // public void OnHoverExit()
    // {
    //     hitObject.GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0f);
    // }

    private void SoundOn()
    {
        if (_playSound == true)
        {
            //Play the audio you attach to the AudioSource component
            m_MyAudioSource.Play();
            //Ensure audio doesn�t play more than once
            Task.Delay(1000);
            //m_MyAudioSource.Stop();
            _playSound = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Collectible")
        {
            collider.gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 1.5f);
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Collectible")
        {
            collider.gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0f);
        }
    }
}
