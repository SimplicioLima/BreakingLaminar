using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool inDebug = true;

    //Camera mission;
    [SerializeField] private bool cctvOn = true;

    //Inventory
    [SerializeField] private GameObject _invCanvas;
    private bool _isActive = false;

    void Start()
    {

        //Inventory
        _invCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cctvOn = !cctvOn;
            CCTVController.ChangeValue(cctvOn);

            if (inDebug) Debug.Log("CCTVController.camerasOn :" + CCTVController.camerasOn);
        }
        ActivateInv();
    }

    public void ActivateInv()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!InventoryManager._isActive)
            {
                _invCanvas.SetActive(true);
                InventoryManager._isActive = true;
            }
            else
            {
                InventoryManager._isActive = false;
                _invCanvas.SetActive(false);
            }
        }
    }

}
