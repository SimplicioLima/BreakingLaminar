using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigma1 : MonoBehaviour
{
    public bool inDebug = true;
    [Header("Grid 5x5 max !")]
    [SerializeField] private bool gameOn = true;
    [SerializeField] private List<GameObject> Cubes = new List<GameObject>();

    [SerializeField] private List<Material> cubeColors = new List<Material>();
    
    private Camera cam;
    [SerializeField] private float distance;
    private bool solved = false;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOn) ChangeCubeColor();
        //if (solved) anim.SetBool("isOpen", solved);
        //else anim.SetBool("isOpen", solved);
        if (solved) CCTVController.AtivateCam = true;
    }

    private void ChangeCubeColor()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "EnigmaCube")
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (inDebug) Debug.Log("Enter on E" + hit.collider.gameObject.name);
                    foreach (var item in Cubes)
                    {
                        if(hit.collider.gameObject.transform.position == item.transform.position)
                        {
                            if (hit.collider.gameObject.GetComponent<cubeInigma>().selected == true)
                            {
                                hit.collider.gameObject.GetComponent<cubeInigma>().selected = false;
                                hit.collider.gameObject.GetComponentInChildren<MeshRenderer>().material = cubeColors[1];
                            }
                            else
                            {
                                hit.collider.gameObject.GetComponent<cubeInigma>().selected = true;
                                hit.collider.gameObject.GetComponentInChildren<MeshRenderer>().material = cubeColors[0];
                            }
                        }
                    }

                    //Botao de comfirmar
                    if (hit.collider.name == "Vereficar")
                    {
                        solved = VerifyCondition();
                        if (solved) anim.SetBool("isOpen", solved);
                    }
                    //Botao de resetar
                    //if (hit.collider.name == "Resetar")
                    //{
                    //    EndAnim();
                    //    anim.SetBool("isOpen", solved);
                    //}
                }
            }
        }
    }

    private bool VerifyCondition()
    {
        int countCode = 0;
        int countSelected = 0;

        for (int i = 0; i < Cubes.Count; i++)
        {
            if(Cubes[i].gameObject.GetComponent<cubeInigma>().selected == true)
            {
                countCode++;
            }
            if (Cubes[i].gameObject.GetComponent<cubeInigma>().secretCode == true)
            {
                countSelected++;
            }
        }

        bool valueCorrect = (countCode == countSelected);

        foreach (var item in Cubes)
        {
            if (item.gameObject.GetComponent<cubeInigma>().selected != item.gameObject.GetComponent<cubeInigma>().secretCode && item.gameObject.GetComponent<cubeInigma>().secretCode == true)
            {
                return false;
            }
        }

        if (valueCorrect)
        { //Condiçao codigo certo

            foreach (var item in Cubes)
            {
                item.gameObject.GetComponent<MeshRenderer>().material = cubeColors[2];
                item.gameObject.GetComponent<MeshCollider>().enabled = false;
            }

            return true;
        }
        return false;
    }

    private void ResetEnigma()
    {
        foreach (var item in Cubes)
        {
            item.gameObject.GetComponent<MeshRenderer>().material = cubeColors[1];
            item.gameObject.GetComponent<cubeInigma>().selected = false;
        }
    }

    private void EndAnim()
    {
        ResetEnigma();
        solved = false;
        anim.SetBool("isOpen", false);
    }
}
