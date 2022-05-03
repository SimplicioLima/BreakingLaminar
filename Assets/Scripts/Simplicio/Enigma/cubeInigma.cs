using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeInigma : MonoBehaviour
{
    public Vector3 position;
    public bool selected = false;
    public bool secretCode = false;
    public cubeInigma(Vector3 pos, bool selected, bool secretCode)
    {
        this.selected = selected;
        this.position = pos;
        this.secretCode = secretCode;
    }
}
