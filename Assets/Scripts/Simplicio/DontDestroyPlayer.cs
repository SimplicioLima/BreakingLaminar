using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyPlayer : MonoBehaviour
{
    public static DontDestroyPlayer current { get; private set; }
    //Para evitar a perda dos obj
    void Start()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
