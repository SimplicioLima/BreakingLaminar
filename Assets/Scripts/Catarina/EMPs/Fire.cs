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
            projectilePrefab.GetComponent<Rigidbody>().isKinematic = true;
            projectilePrefab.GetComponent<Rigidbody>().useGravity = false;
            Vector3 posObj = new Vector3(spawnPrefab.position.x, spawnPrefab.position.y + 2, spawnPrefab.position.x);
            GameObject projectile = Instantiate(projectilePrefab, spawnPrefab.position, fpsCamera.transform.rotation);
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().useGravity = true;
            Vector3 dirForce = new Vector3(fpsCamera.transform.forward.x, 0.6f, fpsCamera.transform.forward.z);
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
