using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectingManager : MonoBehaviour
{
    public bool inDebug = false;
    [SerializeField] private string DrawersTag = "Drawer";
    [SerializeField] private bool _isOpen = false;

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject OpenClose;
    [SerializeField] private Text PanelText;
    private string OpenText = "Press E to open";
    private string CloseText = "Press E to close";

    [SerializeField] private Transform Player;
    [SerializeField] private float CanOpenRadius;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SeleteIt();
    }

    private void SeleteIt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Transform selection = hit.transform;
            //if (inDebug) Debug.Log("Selected distance:" + selection.tag);

            if (hit.collider.tag == DrawersTag)
            {
                //Logic to open and close
                float dist = Vector3.Distance(Player.position, selection.position);

                if (inDebug) Debug.Log("Selected distance:" + dist);

                if (dist < CanOpenRadius)
                {
                    anim = hit.collider.gameObject.GetComponentInChildren<Animator>();
                    //Caso precisse de timer, por a funçao como async e dar task.delay(x segundos)
                    OpenDrawer();
                }
            }
        }
    }

    private void OpenDrawer()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _isOpen = !_isOpen;
            anim.SetBool("isOpen", _isOpen);
        }

        SetMessage();
    }

    private void SetMessage()
    {
        if (_isOpen) PanelText.text = CloseText;
        else PanelText.text = OpenText;
    }

}
