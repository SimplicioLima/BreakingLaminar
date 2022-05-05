using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithTerminal : MonoBehaviour
{
    public GameObject normalCanvas;
    public GameObject terminalCanvas;
    public GameObject interactUI;
    public GameObject fpsController;
    public Camera temporaryCamera;
    private bool activeUI;
    bool visible;
    // Start is called before the first frame update
    void Start()
    {
        activeUI = false;
        interactUI.SetActive(false);
        visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeUI)
        {
            interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                visible = true;
                normalCanvas.SetActive(false);
                terminalCanvas.SetActive(true);
                fpsController.SetActive(false);
                temporaryCamera.gameObject.SetActive(true);
               
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                visible = false;
                normalCanvas.SetActive(true);
                terminalCanvas.SetActive(false);
                fpsController.SetActive(true);
                temporaryCamera.gameObject.SetActive(false);
                
            }
        }
        else
        {
            interactUI.SetActive(false);
        }

        if(visible == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Player")
        {
            activeUI = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            activeUI = false;
        }
    }
}
