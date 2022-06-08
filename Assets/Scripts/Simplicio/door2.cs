using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class door2 : MonoBehaviour
{
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
    }

    void Update()
    {
        if (_triggerMessage) OpenDoor();
    }


    /// <summary>
    /// Acresentar uma variavel static para tipo de som(intensidade) para show na ui
    /// </summary>

    private void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isOpen = !_isOpen;
            anim.SetBool("isOpen", _isOpen);
            //OpenClose.SetActive(false);
            if (!_isOpen) OpenClose.SetActive(false);
            if (this.gameObject == MissionController.current.missionObj[3])
            {
                MissionController.current.mission4_Cargo = true;
            }
            if (this.gameObject == MissionController.current.missionObj[5])
            {
                MissionController.current.mission8_OpenServer = true;
            }

        }

        SetMessage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenClose.SetActive(true);
            _triggerMessage = true;

            //else
            //{
            //    OpenClose.SetActive(false);
            //    _triggerMessage = false;
            //}
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


    private void SetMessage()
    {
        if (_isOpen) PanelText.text = CloseText;
        else PanelText.text = OpenText;
    }
}
