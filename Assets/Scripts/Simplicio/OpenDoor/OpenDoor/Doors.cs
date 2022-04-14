using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private List<string> doors = new List<string>();
    [SerializeField] private int Ndoors;

    //Colocar o obj com a tag "DoorBell"
    private bool _ActiveHoldPos = false;

    //Set default pos para clicar no button
    [SerializeField] private Transform player;
    [SerializeField] private Transform lockPosition;
    [SerializeField] private Transform keyButton;

    //Cam && Raycast
    [SerializeField] private Transform cam;

    //States
    private static bool _isOpen = false;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject OpenClose;
    [SerializeField] private Text PanelText;

    private string OpenText = "Press E to open";
    private string CloseText = "Press E to close";
    private bool _triggerMessage = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SetMessage();
        //AddDoors(Ndoors);
    }

    private void Update()
    {
        LookAndLockPos();

        if (_ActiveHoldPos && Input.GetKeyDown(KeyCode.E))
        {
            player.position = lockPosition.position;
            player.transform.forward = keyButton.right;
            SetMessage();
            _ActiveHoldPos = !_ActiveHoldPos;
        }

        if(_triggerMessage && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
            SetMessage();
        }
    }

    private void AddDoors(int nDoors)
    {
        for (int i = 1; i < nDoors; i++)
        {
            doors.Add($"Door{i}");
        }
    }

    private void LookAndLockPos()
    {
        SetMessage();
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 3))
        {
            foreach (var item in doors)
            {
                if (hit.collider.gameObject.name == item)
                {
                    _ActiveHoldPos = true;
                }
            }
        }
    }

    private void OpenDoor()
    {
        _isOpen = !_isOpen;
        anim.SetBool("isOpen", _isOpen);
    }

    private void SetMessage()
    {
        if (_isOpen) PanelText.text = CloseText;
        else PanelText.text = OpenText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenClose.SetActive(true);
            _triggerMessage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenClose.SetActive(false);
            _triggerMessage = false;
        }
    }


}
