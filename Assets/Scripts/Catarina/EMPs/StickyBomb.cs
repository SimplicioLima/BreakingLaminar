using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    public delegate IEnumerator DisableEnemy();
    public static event DisableEnemy OnEnemyDisabled;
    public static float waitForReEnable = 3f; 



    //bool samePosition = false;
    private void OnCollisionEnter(Collision collision)
    {
        
        // posteriormente adicionar o if sticky bomb -> else (objeto normal) dont do anything
        this.GetComponent<Rigidbody>().isKinematic = true; // to stick
        this.gameObject.GetComponent<Collider>().isTrigger = true;

        if(collision.transform.tag == "Enemy")
        {
            /*
            while(samePosition == true)
            {
                gameObject.transform.Translate(collision.transform.forward);
            }*/

            //if(Input.GetKeyDown(KeyCode.G))´´
            OnEnemyDisabled();
            EnemyStateMachine.HitBySticky = true;
            StartCoroutine(SetFalse());
            //SimpleMovement.move = false;
            
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(waitForReEnable);
        Destroy(gameObject);
        EnemyStateMachine.HitBySticky = false;
        //samePosition = false;
        //SimpleMovement.move = true;
    }
}
