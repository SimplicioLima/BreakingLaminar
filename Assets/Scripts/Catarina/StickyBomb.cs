using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    bool samePosition = false;
    private void OnCollisionEnter(Collision collision)
    {
        
        // posteriormente adicionar o if sticky bomb -> else (objeto normal) dont do anything
        this.GetComponent<Rigidbody>().isKinematic = true; // to stick
        this.gameObject.GetComponent<Collider>().isTrigger = true;

        if(collision.transform.tag == "Enemy")
        {
            while(samePosition == true)
            {
                gameObject.transform.Translate(collision.transform.forward);
            }
            
            if(Input.GetKeyDown(KeyCode.G))
            {
                SimpleMovement.move = false;
                StartCoroutine(SetFalse()); 
            }
            
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        samePosition = false;
        SimpleMovement.move = true;
    }
}
