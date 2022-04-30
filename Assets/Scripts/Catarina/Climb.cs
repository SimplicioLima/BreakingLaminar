using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Climb : MonoBehaviour
{
    public GameObject interactUI;
    bool activeUI;
    // Start is called before the first frame update
    void Start()
    {
        activeUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeUI)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Player")
        {
            activeUI = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            activeUI = false;
            interactUI.SetActive(false);
        }
    }
}
