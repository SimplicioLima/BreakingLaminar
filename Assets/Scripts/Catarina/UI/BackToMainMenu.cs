using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    // Update is called once per frame
    public void Back()
    {
        SceneManager.LoadScene(3);
    }
}
