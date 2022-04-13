using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyControledOpenDoor : MonoBehaviour
{
    public bool inDebug = true;
    //States for Animator
    private bool _isOpen = false;
    private bool _key1 = false;
    private bool _key2 = false;
    private bool _key3 = false;

    //Manager state
    public bool canOpen = false;

    [SerializeField] private Animator anim;
    [SerializeField] private Animator anim2;

    [SerializeField] private GameObject OpenClose;
    [SerializeField] private Text PanelText;

    private string OpenText = "Press E to open";
    private string CloseText = "Is opening";
    private bool _triggerMessage = false;

    void Start()
    {
        anim.GetComponent<Animator>();
        anim2.GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (canOpen && _triggerMessage)
        {
            OpenDoor();
        }
    }

    //Buscar a info ao gameManager;
    public bool GetPlayerHasAllKeys()
    {
        //Saber se o player tem na posse dele/ ou no inventario as 3 chaves para abrir a porta
        return canOpen;
    }

    private void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
        {
            //Primeira chave a rodar
            float rnd1 = Random.Range(1.0f, 3.9f);

            if (rnd1 < 1.5f)
            {
                _key1 = true;
                anim.SetBool("key1used", _key1);
                if (inDebug) Debug.Log("KEY1 " + _key1);
            }
            else if (rnd1 < 3.0f && rnd1 > 2.0f)
            {
                _key2 = true;
                anim.SetBool("key2used", _key2);
                if (inDebug) Debug.Log("KEY2 " + _key2);
            }
            else
            {
                _key3 = true;
                anim.SetBool("key3used", _key3);
                if (inDebug) Debug.Log("KEY3 " + _key3);
            }

            //Segunda chave a correr
            float rnd2 = Random.Range(2.0f, 3.9f);
            if (_key1)
            {
                if (rnd2 < 3.0f)
                {
                    _key2 = true;
                    anim.SetBool("key2used", _key2);
                    if (inDebug) Debug.Log("KEY2 " + _key2);
                }
                else
                {
                    _key3 = true;
                    anim.SetBool("key3used", _key3);
                    if (inDebug) Debug.Log("KEY3 " + _key3);
                }
            }
            else if (_key2)
            {
                if (rnd2 < 3.0f)
                {
                    _key1 = true;
                    anim.SetBool("key1used", _key1);
                    if (inDebug) Debug.Log("KEY1 " + _key1);
                }
                else
                {
                    _key3 = true;
                    anim.SetBool("key3used", _key3);
                    if (inDebug) Debug.Log("KEY3 " + _key3);
                }
            }
            else if (_key3)
            {
                if (rnd2 < 3.0f)
                {
                    _key1 = true;
                    anim.SetBool("key1used", _key1);
                    if (inDebug) Debug.Log("KEY1 " + _key1);
                }
                else
                {
                    _key3 = true;
                    anim.SetBool("key2used", _key2);
                    if (inDebug) Debug.Log("KEY2 " + _key2);
                }
            }

            //Ultima Chave
            if (_key1 && _key2)
            {
                _key3 = true;
                anim.SetBool("key3used", _key3);
                if (inDebug) Debug.Log("KEY3 " + _key3);
            }
            else if (_key2 && _key3)
            {
                _key1 = true;
                anim.SetBool("key1used", _key1);
                if (inDebug) Debug.Log("KEY1 " + _key1);
            }
            else
            {
                _key2 = true;
                anim.SetBool("key2used", _key2);
                if (inDebug) Debug.Log("KEY2 " + _key3);
            }

            _isOpen = true;
            anim2.SetBool("isOpen", _isOpen);
        }
        else if(Input.GetKeyDown(KeyCode.E) && _isOpen)
        {
            _key1 = false;
            _key2 = false;
            _key3 = false;
            _isOpen = false;
            anim.SetBool("key1used", _key1);
            anim.SetBool("key2used", _key2);
            anim.SetBool("key3used", _key3);
            anim2.SetBool("isOpen", _isOpen);
        }
        SetMessage();
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

    //Show message on screen
    private void SetMessage()
    {
        if (_isOpen) PanelText.text = CloseText;
        else PanelText.text = OpenText;
    }
}
