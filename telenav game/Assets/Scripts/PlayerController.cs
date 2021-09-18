using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private CharacterController characterController;
    private Animator animator;

    private Vector3 playerVelocity;
    private bool grounded;
    public bool grounded2;
    public float gravityValue = -9.81f;

    public float moveSpeed;
    private float currentMoveSpeed;
    public float jumpHeight;

    public float jumpCD;
    private float jumpCDCounter;

    public float patCD;
    private float patCDCounter;
    public float hugCD;
    private float hugCDCounter;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = characterController.isGrounded;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = CameraController.instance.transform.right * Input.GetAxis("Horizontal") + Vector3.ProjectOnPlane(CameraController.instance.transform.forward, Vector3.up).normalized * Input.GetAxis("Vertical");
        characterController.Move(Vector3.ClampMagnitude(move, 1) * Time.deltaTime * currentMoveSpeed);
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButton("Jump") && grounded2 && jumpCDCounter <= 0)
        {
            jumpCDCounter = jumpCD;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        //Attacks
        if (Input.GetButtonDown("Fire1") && patCDCounter <= 0)
        {
            patCDCounter = patCD;
            animator.SetTrigger("pat");
        }
        if (Input.GetButtonDown("Fire2") && hugCDCounter <= 0)
        {
            hugCDCounter = hugCD;
            animator.SetTrigger("hug");
        }

        //Animator
        animator.SetFloat("move", move.sqrMagnitude);
        animator.SetFloat("yVelocity", characterController.velocity.y / 4);
        animator.SetBool("grounded", grounded2);

        if (jumpCDCounter > 0)
        {
            jumpCDCounter -= Time.deltaTime;
        }
        if (patCDCounter > 0)
        {
            patCDCounter -= Time.deltaTime;
        }
        if (hugCDCounter > 0)
        {
            hugCDCounter -= Time.deltaTime;
        }
    }
}
