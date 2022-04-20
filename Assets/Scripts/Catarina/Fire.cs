using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Transform spawnPrefab;
    [SerializeField] private float force = 25000;
    [SerializeField] private float speed = 3.2f;
    private Vector3 gravity = new Vector3(0, -9.8f, 0);
    private bool dontShoot = true;

    private void Start()
    {
        //force = (projectilePrefab.GetComponent<Rigidbody>().mass * 9.8f) * speed;
        projectilePrefab.GetComponent<Rigidbody>().AddForceAtPosition(gravity * speed, projectilePrefab.gameObject.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if(dontShoot) Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectile = (GameObject)Instantiate(projectilePrefab, spawnPrefab.position, fpsCamera.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.forward * force, ForceMode.Force);
            /*GameObject projectile = (GameObject) Instantiate(projectilePrefab, fpsCamera.transform.position + new Vector3(1.2f,0,1.2f), fpsCamera.transform.rotation)*/
            //projectile.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.forward * 1800);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        dontShoot = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        dontShoot = true;
    }
}
