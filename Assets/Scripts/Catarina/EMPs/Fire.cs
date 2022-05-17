using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Fire : MonoBehaviour
{
    //Debuging
    [SerializeField] private bool inDebug = true;
    //private GameObject projectilePrefab;
    [HideInInspector] private Camera fpsCamera;
    [SerializeField] private Transform spawnPrefab;

    [SerializeField] private float force = 8000;
    private bool dontShoot = true;

    void Update()
    {
        if(dontShoot) Shoot();
        fpsCamera = Camera.main;
    }

    public void Shoot()
    {
        if (GameManager.current.CanShoot() && Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectilePrefab = GameManager.current.ChangeFireObject();
            projectilePrefab.GetComponent<Rigidbody>().isKinematic = false;
            projectilePrefab.GetComponent<Rigidbody>().useGravity = true;
            GameObject projectile = Instantiate(projectilePrefab, spawnPrefab.position, fpsCamera.transform.rotation);
            Vector3 dirForce = new Vector3(fpsCamera.transform.forward.x, 0.5f, fpsCamera.transform.forward.z);
            projectile.GetComponent<Rigidbody>().AddForce(dirForce * force, ForceMode.Force);

            GameManager.current.RemoveFireObjFromInv();
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
