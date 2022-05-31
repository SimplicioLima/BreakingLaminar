using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanController : MonoBehaviour
{
    private Vector2 offsetvalue = new Vector2(0,-0.52f);
    private bool Active1 = false;
    private bool Active2 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetScan(Active1,Active2);
    }

    public void SetScan(bool active1, bool active2)
    {
        if (active1)
        {
            if (offsetvalue.y >= 1.00f) Active1 = false;
            else offsetvalue.y += 0.05f;
            Material mat = this.gameObject.GetComponent<MeshRenderer>().material;
            mat.SetTextureOffset("_NightTex", offsetvalue);
        }
        else if (active2)
        {
            if (offsetvalue.y < -1.00f) Active2 = false;
            else offsetvalue.y -= 0.05f;
            Material mat = this.gameObject.GetComponent<MeshRenderer>().material;
            mat.SetTextureOffset("_NightTex", offsetvalue);
        }
    }

    public bool ScanOnRed(bool activate)
    {
        return Active1 = activate;
    }
    public bool ScanOnGreen(bool activate2)
    {
        return Active2 = activate2;
    }

}
