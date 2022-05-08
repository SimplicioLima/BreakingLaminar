using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AleskyController : MonoBehaviour
{
    //Estados do personagem
    [Space]
    [Header ("Player States")]

    public static bool _idle = false;
    public static bool _crouching = false;
    public static bool _walking = false;
    public static bool _throwing = false;
    public static bool _holdingThrow = false;
    public static bool _sprinting = false;

    [Space]
    [Header("Player info")]
    [SerializeField] private Transform player;
    private Vector3 dir;
    private Rigidbody rb;

    private float rY;
    private float rX;
    private float speed = 5; //Dont touch this or line 76
    [SerializeField] private float speedMultiplier = 17.0f;
    private bool canSprint = true;


    //[SerializeField] private Transform camPivot;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform headHeight;

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
        float x = headHeight.position.x + headHeight.forward.x;
        float z = headHeight.position.z + headHeight.forward.z;

        cam.position = new Vector3(x, headHeight.position.y, z);

        //Movement();

        CheckPlayerState();
        CheckAnimationWalking();
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    private void Movement()
    {
        dir = player.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);

        rX = Mathf.Lerp(rX, Input.GetAxisRaw("Mouse X") * 2, 100 * Time.deltaTime);
        rY = Mathf.Clamp(rY - (Input.GetAxisRaw("Mouse Y") * 2 * 65 * Time.deltaTime), -35, 35); // Valor max de movimento y do mouse 

        player.Rotate(0, rX, 0, Space.World);
        cam.rotation = Quaternion.Lerp(cam.rotation, Quaternion.Euler(rY * 2, player.eulerAngles.y , 0), 100 * Time.deltaTime);

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            speed = 5.0f; //alterar se mexer na variable speed;
            _sprinting = true;
        }
        else 
        { 
            speed = 2.0f;
            _sprinting = false;
        }

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

        //Crouch Verefy
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            _crouching = false;
            //if(!_walking) camPivot.position = new Vector3(camPivot.position.x, 4.5f, camPivot.position.z);
        }
        else
        {
            _crouching = true;
            //camPivot.position = new Vector3(camPivot.position.x, 2.0f, camPivot.position.z);
        }

        //Throw object Verefy
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            _holdingThrow = true;
        }
        else _holdingThrow = false;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _throwing = true;
        }
        else _throwing = false;
    }

    private void CheckAnimationWalking()
    {
        if (_walking && _crouching)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isCrouch", true);
            anim.SetBool("isIdle", false);

            speed = speedMultiplier;
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

            speed = 3.0f;
        }

        if (_throwing && _crouching)
        {
            anim.SetBool("isCrouch", true);
            anim.SetBool("isThrowing", true);
        }
        else if (_throwing)
        {
            anim.SetBool("isThrowing", true);
            anim.SetBool("isIdle", true);
        }
        else
        {
            anim.SetBool("isThrowing", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("isOpening");
        }

        //Walking
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) anim.SetFloat("walk", 0.5f);
        else if (Input.GetKeyDown(KeyCode.D) && !_sprinting) anim.SetFloat("walk", 0f);
        else if (Input.GetKeyDown(KeyCode.A) && !_sprinting) anim.SetFloat("walk", 1);

        //Running
        anim.SetBool("isRunning", _sprinting);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        canSprint = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        canSprint = true;
    }

}
