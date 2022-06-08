using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading.Tasks;
using System;
//using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    //Sound
    private AudioSource m_MyAudioSource;
    //[SerializeField] private AudioClip movingDoors;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool _playSound;

    //States
    public bool _isOpen = false;

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
    private RaycastHit hit;

    [SerializeField] private GameObject scan;
    [SerializeField] private GameObject scan1;

    void Start()
    {
        cam = Camera.main;
        uiMessage.gameObject.SetActive(false);
        m_MyAudioSource = this.gameObject.GetComponent<AudioSource>();
        _playSound = false;
    }

    void Update()
    {
        OpenDoor();

        if (GameManager.current.cctvOff == true) doorsUnlock = true;

        SoundOn();
    }


    private void OpenDoor()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            if (hit.transform.position == this.gameObject.transform.position && !MissionController.current.mission7_DoorKeys)
            {
                if (GameManager.current.haveCaptainAccess == true && captainAccess == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _isOpen = !_isOpen;
                        //SetMessage();
                        anim.SetBool("isOpen", _isOpen);
                        _playSound = true;

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
                else if (GameManager.current.haveBasicAccess == true && basicAccess == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Scaning();
                        scan.gameObject.GetComponent<scanController>().ScanOnRed(true); 
                        scan1.gameObject.GetComponent<scanController>().ScanOnRed(true);
                        //await Task.Delay(400);
                        //scan.gameObject.GetComponent<scanController>().ScanOnRed(false);
                        //
                        _isOpen = !_isOpen;
                        //SetMessage();

                        anim.SetBool("isOpen", _isOpen);
                        _playSound = true;
                        
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
                        //Scaning();
                        scan.gameObject.GetComponent<scanController>().ScanOnGreen(true);
                        scan1.gameObject.GetComponent<scanController>().ScanOnGreen(true);
                        //await Task.Delay(400);
                    }
                    else if (doorsUnlock)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            _isOpen = !_isOpen;
                            //SetMessage();
                            anim.SetBool("isOpen", _isOpen);
                            _playSound = true;
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
                }
                else
                {
                    ShowMessage();
                }
            }
            else if (hit.transform.position == this.gameObject.transform.position && MissionController.current.mission7_DoorKeys)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _isOpen = !_isOpen;
                    //SetMessage();
                    anim.SetBool("isOpen", _isOpen);
                    _playSound = true;
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
            else uiMessage.gameObject.SetActive(false);
        }
    }

    private void SoundOn()
    {
        if (_playSound == true)
        {
            //Play the audio you attach to the AudioSource component
            m_MyAudioSource.Play();
            _playSound = false;
        }
    }

    //se funcionar escrever igual para mostar "press E para interact"
    private async void ShowMessage()
    {
        await Task.Delay(500);
        foreach (var item in InventorySystem.current.inventory)
        {
            if (item.data.id == 13)
            {
                captainAccess = true;
                doorsUnlock = true;
                basicAccess = true;
            }
        }
    }
}