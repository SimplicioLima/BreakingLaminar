using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStickyBomb : MonoBehaviour
{
    public float radius = 2f;

    public delegate IEnumerator DisableEnemyGroup();
    public static event DisableEnemyGroup OnMultipleDisabled;
    public static float waitForReEnable = 3f;


    private void OnCollisionEnter(Collision collision)
    {

        // posteriormente adicionar o if sticky bomb -> else (objeto normal) dont do anything
        this.GetComponent<Rigidbody>().isKinematic = true; // to stick
        this.gameObject.GetComponent<Collider>().isTrigger = true;

        Collider[] robotColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in robotColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                OnMultipleDisabled();
                EnemyStateMachine.HitByGrenade = true;
                //SimpleMovement.move = false;
                StartCoroutine(SetFalse());
            }
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(waitForReEnable);
        EnemyStateMachine.HitByGrenade = false;
        //Destroy(gameObject);
        //SimpleMovement.move = true;


    }
}
