using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverObject : MonoBehaviour
{
    bool mouseOver = false;
    
    void Update()
    {
        if(mouseOver == true)
            GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 1.5f);
        else
            GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0f);
    }
    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
