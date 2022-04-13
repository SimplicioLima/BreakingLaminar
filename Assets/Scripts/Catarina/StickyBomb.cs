using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // posteriormente adicionar o if sticky bomb -> else (objeto normal) dont do anything
        this.GetComponent<Rigidbody>().isKinematic = true; // to stick
        this.gameObject.GetComponent<Collider>().isTrigger = true;

        Invoke("Deactivate", 3); 
    }

    void Deactivate(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);

            // para o real desativar talvez só dar um booleano ao inimigos e condicionar o seu update a partir disso?
        }
        
    }

}
