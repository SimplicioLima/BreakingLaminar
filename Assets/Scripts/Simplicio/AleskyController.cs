using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AleskyController : MonoBehaviour
{
    //Estados do personagem
    [Space]
    [Header("Player States")]
    private bool _idle = false;
    public bool _crouching = false;
    public bool _walking = false;
    private bool _throwing = false;
    private bool _holdingThrow = false;

    [Space]
    [Header("Player info")]
    [SerializeField] private Transform player;
    private Vector3 dir;
    private Rigidbody rb;

    private float rY;
    private float rX;
    private float speed = 10; //Dont touch this
    [SerializeField] private float speedMultiplier = 20.0f;


    [SerializeField] private Transform camPivot;
    [SerializeField] private Transform cam;


    //Animator
    public Animator anim;

    void Start()
    {
        //Mouse info
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();

        CheckPlayerState();
        CheckAnimationWalking();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    private void Movement()
    {
        dir = player.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);

        rX = Mathf.Lerp(rX, Input.GetAxisRaw("Mouse X") * 2, 100 * Time.deltaTime);
        rY = Mathf.Clamp(rY - (Input.GetAxisRaw("Mouse Y") * 2 * 100 * Time.deltaTime), -40, 40); // Valor max de movimento y do mouse 

        player.Rotate(0, rX, 0, Space.World);
        cam.rotation = Quaternion.Lerp(cam.rotation, Quaternion.Euler(rY * 2, player.eulerAngles.y, 0), 100 * Time.deltaTime);

        //Crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _crouching = true;
        }

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedMultiplier;
        }
        else speed = 6.0f;
    }

    private void CheckPlayerState()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            _walking = false;
            _idle = true;
        }
        else
        {
            _walking = true;
            _idle = false;
        }

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            _crouching = false;
        }
    }

    private void CheckAnimationWalking()
    {
        if (_walking && _crouching)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isCrouch", true);
            anim.SetBool("isIdle", false);
        }
        else if (_walking && !_crouching)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isCrouch", false);
            anim.SetBool("isIdle", false);

            //Walking
            anim.SetFloat("walkingDir", 0.3f);
        }
        else if(_crouching && !_walking)
        {
            anim.SetBool("isCrouch", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isCrouch", false);
            anim.SetBool("isIdle", true);
        }
    }
}
