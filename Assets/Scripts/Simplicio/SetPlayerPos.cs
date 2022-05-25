using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPlayerPos : MonoBehaviour
{
    private bool level1 = false;
    private bool enterOnce = false;
    private bool enterOnce2 = false;
    private GameObject fpsController;

    private void Start()
    {
        fpsController = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 1 && level1 && !enterOnce)
        {
            enterOnce2 = false;
            fpsController.transform.position = this.transform.position;
        }
        else if(sceneIndex == 2 && !enterOnce2)
        {
            enterOnce = false;
            fpsController.transform.position = this.transform.position;
        }
        
    }
    
}
