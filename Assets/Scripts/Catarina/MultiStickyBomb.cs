using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStickyBomb : MonoBehaviour
{
    public float radius = 20f;

    void Start()
    {

    }

    void Update()
    {
        Collider[] robotColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in robotColliders)
        {
            if(hitCollider.tag == "Enemy")
            {
                SetFalse();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        // posteriormente adicionar o if sticky bomb -> else (objeto normal) dont do anything
        this.GetComponent<Rigidbody>().isKinematic = true; // to stick
        this.gameObject.GetComponent<Collider>().isTrigger = true;

        if(collision.transform.tag == "Enemy")
        { 
            SimpleMovement.move = false;
            StartCoroutine(SetFalse());
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        SimpleMovement.move = true;

        
    }
}
