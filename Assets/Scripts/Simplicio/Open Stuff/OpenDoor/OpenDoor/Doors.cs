using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //States
    private bool _isOpen = false;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject OpenClose;
    [SerializeField] private Camera cam;
    [SerializeField] private int distance = 10;

    [SerializeField] private Text PanelText;
    private string OpenText = "Press E to open";
    private string CloseText = "Press E to close";
    [SerializeField] private List<Material> mat = new List<Material>();

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        OpenClose.SetActive(false);
        
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
                    if (_isOpen) gameObject.transform.GetComponent<MeshRenderer>().material = mat[1];
                    else gameObject.transform.GetComponent<MeshRenderer>().material = mat[0];
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