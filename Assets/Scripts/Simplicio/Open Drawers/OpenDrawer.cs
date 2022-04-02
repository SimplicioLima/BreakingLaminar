using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    public bool inDebug = true;
    [SerializeField] private Animator anim;
    private bool _isOpen = false;
    private bool _CanBeOpen = false;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Selecting();
    }


    private void Selecting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<MeshRenderer>().material = mat;
                anim.SetBool("isOpen", true);
                Transform selection = hit.transform;
                if (inDebug) Debug.Log(selection.name + " | " + this.gameObject.name);

                if (selection.name == this.gameObject.name && !_isOpen)
                {
                    _isOpen = false;
                    anim.SetBool("isOpen", true);
                    if (inDebug) Debug.Log("its open " + anim.GetBool("isOpen"));
                }
                else if (selection.name == this.gameObject.name && _isOpen)
                {
                    _isOpen = true;
                    anim.SetBool("isOpen", false);
                    if (inDebug) Debug.Log("its closed " + anim.GetBool("isOpen"));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _CanBeOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _CanBeOpen = false;
        }
    }
}
