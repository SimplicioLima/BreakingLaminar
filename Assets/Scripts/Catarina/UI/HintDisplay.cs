using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintDisplay : MonoBehaviour
{
    private int random;
    public GameObject hintDisplay;
    bool genHint = false;
    void Update()
    {
        if(genHint == false)
        {
            hintDisplay.GetComponent<Animator>().Play("New State");
            genHint = true;
            StartCoroutine(HintTracker());
        }
    }

    IEnumerator HintTracker()
    {
        random = Random.Range(1,5);

        if(random == 1)
        {
            hintDisplay.GetComponent<Text>().text = "Oiling the robotic arms...";
        }
        if(random == 2)
        {
            hintDisplay.GetComponent<Text>().text = "Scattering upgrade parts...";
        }
        if(random == 3)
        {
            hintDisplay.GetComponent<Text>().text = "Polishing the missils...";
        }
        if(random == 4)
        {
            hintDisplay.GetComponent<Text>().text = "Testing the big, red button...";
        }
        if(random == 5)
        {
            hintDisplay.GetComponent<Text>().text = "Controlling the buoyancy...";
        }

        hintDisplay.GetComponent<Animator>().Play("HintTextAnim");

        yield return new WaitForSeconds(9);

        genHint = false;
    }
}
