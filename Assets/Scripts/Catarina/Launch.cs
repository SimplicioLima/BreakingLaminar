using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
   public Transform cam;
    public GameObject grenadePref;
    GameObject grenade;
    public Transform grenadeSpawn;

    public float grenadeSpeed = 90;
    public float grenadeLifetime = 4;

    [Space]
    [Header("Explosion")]
    public GameObject explosionEffect;
    public float explosionForce = 10000f;
    public float radius = 20f;
    public float delay = 3f;


    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (grenadeSpeed < 90) grenadeSpeed += 1.75f; else grenadeSpeed = 90;

        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            LaunchGrenade();
        }
    }

    private void LaunchGrenade()
    {
        grenade = Instantiate(grenadePref);

        Physics.IgnoreCollision(grenade.GetComponent<Collider>(), grenadeSpawn.parent.GetComponent<Collider>());

        grenade.transform.position = grenadeSpawn.position;
        Vector3 rotation = grenade.transform.rotation.eulerAngles;

        grenade.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y + 270, rotation.z);

        grenade.GetComponent<Rigidbody>().AddForce(cam.forward * grenadeSpeed, ForceMode.Impulse);

        grenadeSpeed = 60;

    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();
            rig.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);
        }

        //Explosion effect
        
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        //this.gameObject.GetComponent<AudioSource>().Play();
        
        //Destroy(gameObject);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    */
}
