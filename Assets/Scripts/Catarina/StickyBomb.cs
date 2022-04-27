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

        if(collision.transform.tag == "Enemy")
        {
            SimpleMovement.move = false;
            StartCoroutine(SetFalse());
        }
    }

    void Deactivate(Collision collision)
    {
        
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);

            // para o real desativar talvez só dar um booleano ao inimigos e condicionar o seu update a partir disso?
        }
        
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(3); //wait 10 seconds
        SimpleMovement.move = true;
    }

}
