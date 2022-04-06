using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Camera fpsCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectile = (GameObject) Instantiate(projectilePrefab, fpsCamera.transform.position + new Vector3(1.2f,0,1.2f), fpsCamera.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.forward * 1800);
        }
    }
}
