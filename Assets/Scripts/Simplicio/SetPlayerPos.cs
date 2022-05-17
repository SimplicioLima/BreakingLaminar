using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPlayerPos : MonoBehaviour
{
    private bool level1 = false;
    private bool enterOnce = false;

    void Update()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0 && level1 && !enterOnce)
        {
            GameManager.current.player.transform.position = this.transform.position;
        }
        else if (sceneIndex == 1 && !enterOnce)
        {
            GameManager.current.player.transform.position = this.transform.position;
            level1 = true;
        }
        else if(sceneIndex == 2 && !enterOnce)
        {
            GameManager.current.player.transform.position = this.transform.position;
        }
    }
    
}
