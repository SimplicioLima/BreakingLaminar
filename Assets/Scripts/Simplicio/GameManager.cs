using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool inDebug = true;

    [SerializeField] private bool cctvOn = true;

    void Start()
    {
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
    }


}
