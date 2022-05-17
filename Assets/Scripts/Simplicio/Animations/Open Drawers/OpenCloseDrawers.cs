using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenCloseDrawers : MonoBehaviour
{
    //States
    [SerializeField] private bool inDebug = true;
    private bool _isOpen = false;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject OpenClose;
    [SerializeField] private Text PanelText;

    private string OpenText = "Press E to open";
    private string CloseText = "Press E to close";
    //private bool _triggerMessage = false;
    private Camera cam;
    [SerializeField] private int distance = 10;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            if (hit.transform.position == this.gameObject.transform.position)
            {
                OpenClose.SetActive(true);
                SetMessage();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _isOpen = !_isOpen;
                    SetMessage();
                    anim.SetBool("isOpen", _isOpen);
                }
            }
            else
            {
                SetMessage();
                OpenClose.SetActive(false);
            }
        }
        else
        {
            SetMessage();
            OpenClose.SetActive(false);
        }
    }

    private void SetMessage()
    {
        if (_isOpen) PanelText.text = CloseText;
        else PanelText.text = OpenText;
    }
}
    //private void OpenDoor()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        _isOpen = !_isOpen;
    //        anim.SetBool("isOpen", _isOpen);
    //        //OpenClose.SetActive(false);
    //        if(!_isOpen) OpenClose.SetActive(false);
    //    }

    //    SetMessage();
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        OpenClose.SetActive(true);
    //        _triggerMessage = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        OpenClose.SetActive(false);
    //        _triggerMessage = false;
    //    }
    //}


    //private void SetMessage()
    //{
    //    if (_isOpen) PanelText.text = CloseText;
    //    else PanelText.text = OpenText;
    //}

