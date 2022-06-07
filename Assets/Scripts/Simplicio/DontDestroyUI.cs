using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyUI : MonoBehaviour
{
    public static DontDestroyUI current { get; private set; }
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
