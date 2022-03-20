using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AleskyController : MonoBehaviour
{
    //Estados do personagem
    private bool _idle = false;
    private bool _crouching = false;
    private bool _walking = false;
    private bool _throwing = false;
    private bool _holdingThrow = false;





    //Movement
    public float speed = 0.1f;
    public float rotaionSpeed = 50.0f;

    void Start()
    {
        //Mouse info
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * speed);
        }
        transform.Rotate(0,Input.GetAxis("Mouse X"), 0 * Time.deltaTime * rotaionSpeed);
    }
}
