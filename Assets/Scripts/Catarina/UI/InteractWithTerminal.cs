using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithTerminal : MonoBehaviour
{
    public GameObject normalCanvas;
    public GameObject puzzleCanvas;
    public GameObject interactUI;
    [HideInInspector] public GameObject fpsController;
    public Camera temporaryCamera;
    private bool activeUI;
    bool visible;
    // Start is called before the first frame update
    void Start()
    {
        activeUI = false;
        interactUI.SetActive(false);
        visible = false;
        fpsController = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if(activeUI)
        {
            interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                normalCanvas.SetActive(false);
                puzzleCanvas.SetActive(true);
                fpsController.SetActive(false);
                temporaryCamera.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                
                normalCanvas.SetActive(true);
                puzzleCanvas.SetActive(false);
                fpsController.transform.position = this.transform.position;
                fpsController.SetActive(true);
                temporaryCamera.gameObject.SetActive(false);
            }
        }
        else
        {
            interactUI.SetActive(false);
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
