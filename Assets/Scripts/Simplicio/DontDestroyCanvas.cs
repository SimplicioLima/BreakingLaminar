using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyCanvas : MonoBehaviour
{
    //Para evitar a perda dos obj
    void Start()
    {
        if(SceneManager.sceneCount > 0) 
            DontDestroyOnLoad(gameObject);
    }
}
