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
    [SerializeField] private List<GameObject> screens = new List<GameObject>();

    void Start()
    {
        //anim = GetComponentInChildren<Animator>();
        OpenClose.SetActive(false);
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
                    if (_isOpen)
                    {
                        screens[0].transform.GetComponent<MeshRenderer>().material = mat[1];
                        screens[1].transform.GetComponent<MeshRenderer>().material = mat[1];
                    }
                    else
                    {
                        screens[0].transform.GetComponent<MeshRenderer>().material = mat[0];
                        screens[1].transform.GetComponent<MeshRenderer>().material = mat[0];
                    }
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