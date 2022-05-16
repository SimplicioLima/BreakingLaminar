using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Doors : MonoBehaviour
{
    //States
    [HideInInspector] public bool _isOpen = false;

    [SerializeField] private bool basicAccess = false;
    [SerializeField] private bool captainAccess = false;

    [SerializeField] private Animator anim;
    [SerializeField] private int distance = 10;
    [SerializeField] private List<Material> mat = new List<Material>();
    [SerializeField] private List<GameObject> screens = new List<GameObject>();
    
    [SerializeField] private Text uiMessage;
    private string noCaptainText = "You need Captain Credencial to open this door!";
    private string noBasicText = "You need Basic Credencial to open this door!";

    private Camera cam;
    private int distancia = 6;

    public bool doorsUnlock = false;
    public bool mission7 = false;

    void Start()
    {
        cam = Camera.main;
        uiMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        OpenDoor();
        if(GameManager.current.haveBasicAccess == false || !GameManager.current.haveCaptainAccess == false)
        {
            ShowMessage();
        }
        if(GameManager.current.cctvOff == true) doorsUnlock = true;
    }

    private void OpenDoor()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {

            if (hit.transform.position == this.gameObject.transform.position && !mission7)
            {
                if (doorsUnlock && GameManager.current.haveCaptainAccess == true && captainAccess == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _isOpen = !_isOpen;
                        //SetMessage();
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
                else if (doorsUnlock && GameManager.current.haveBasicAccess == true && basicAccess == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _isOpen = !_isOpen;
                        //SetMessage();
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
                    ShowMessage();
                }
            }
            else if (hit.transform.position == this.gameObject.transform.position && MissionController.current.mission7_DoorKeys)
            {
                if (doorsUnlock && GameManager.current.haveCaptainAccess == true && captainAccess == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _isOpen = !_isOpen;
                        //SetMessage();
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
                else if (doorsUnlock && GameManager.current.haveBasicAccess == true && basicAccess == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _isOpen = !_isOpen;
                        //SetMessage();
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
                    ShowMessage();
                }
            }
            else uiMessage.gameObject.SetActive(false);
        }
    }

    //se funcionar escrever igual para mostar "press E para interact"
    private async void ShowMessage()
    {
        float dist = Vector3.Distance(this.transform.position, Camera.main.transform.position);

        //Change Message
        if (!GameManager.current.haveBasicAccess && basicAccess)
        {
            uiMessage.text = noBasicText;
        }
        else if (!GameManager.current.haveCaptainAccess && captainAccess)
        {
            uiMessage.text = noCaptainText;
        }

        //Verefy distance
        if (dist < distancia)
        {
            uiMessage.gameObject.SetActive(true);
        }
        else
        {
            uiMessage.gameObject.SetActive(false);
        }
        await Task.Delay(2000);
        uiMessage.gameObject.SetActive(false);
    }
}