using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool inDebug = false;

    private AudioSource m_MyAudioSource;
    private bool _playSound;

    //private bool _islookAt = false;
    private ItemObject _lookAtTarget;
    //public Camera cam;
    [SerializeField] private int distance = 10;

    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        _playSound = false;
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Collectible")
            {
                if(hit.collider.gameObject.GetComponent<Outline>() != true)
                {
                    var outline = hit.collider.gameObject.AddComponent<Outline>();
                    //.GetComponent<GameObject>().gameObject.AddComponent<Outline>();

                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = Color.yellow;
                    outline.OutlineWidth = 5f;
                }
                

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
}
