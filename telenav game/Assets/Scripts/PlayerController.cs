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
    public float jumpDashForce;
    public float jumpDashDuration;
    private float jumpDashDurationCounter;
    public float stompForce;

    public float attackAnimStunPercent;
    public float patCD;
    public float patSpeed;
    public float patDashForce;
    public float patDashDuration;
    private float patDashDurationCounter;
    private float patCDCounter;
    public float hugCD;
    public float hugSpeed;
    private float hugCDCounter;
    private float patStun;
    private float patStunCounter;
    private float hugStun;
    private float hugStunCounter;

    public bool stunned;

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

        animator.SetFloat("patSpeed", patSpeed);
        animator.SetFloat("hugSpeed", hugSpeed);

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name.Contains("pat"))
            {
                patStun = clip.length * attackAnimStunPercent / patSpeed;
            }
            else if (clip.name.Contains("hug"))
            {
                hugStun = clip.length * attackAnimStunPercent / hugSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("pat") || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("hug"))
        //{
        //    stunned = true;
        //}
        //else
        //{
        //    stunned = false;
        //}
        if (hugStunCounter > 0 || patStunCounter > 0)
        {
            stunned = true;
        }
        else
        {
            stunned = false;
        }

        if (!stunned)
        {
            grounded = characterController.isGrounded;
            if (grounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                grounded2 = true;
            }

            var horizontalInput = 0f;
            var verticalInput = 0f;
            if (grounded2)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            else
            {
                if (jumpCD - jumpCDCounter > .2f)
                {
                    if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
                    {
                        playerVelocity.y -= Mathf.Sqrt(stompForce * -0.001f * gravityValue);
                    }
                }
            }
            if (grounded || grounded2 && playerVelocity.y < 0)
            {
                jumpDashDurationCounter = 0;
            }

            Vector3 move = CameraController.instance.transform.right * horizontalInput + Vector3.ProjectOnPlane(CameraController.instance.transform.forward, Vector3.up).normalized * verticalInput;
            characterController.Move(Vector3.ClampMagnitude(move, 1) * Time.deltaTime * currentMoveSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            if (Input.GetButton("Jump") && grounded2 && jumpCDCounter <= 0)
            {
                jumpCDCounter = jumpCD;
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumpDashDurationCounter = jumpDashDuration;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);

            //Attacks
            if (grounded2)
            {
                if (Input.GetButtonDown("Fire1") && patCDCounter <= 0 && hugCDCounter <= 0)
                {
                    //gameObject.transform.forward = Vector3.ProjectOnPlane(CameraController.instance.transform.forward, Vector3.up).normalized;
                    patCDCounter = patCD;
                    animator.SetTrigger("pat");
                    patDashDurationCounter = patDashDuration;
                    patStunCounter = patStun;
                }
                if (Input.GetButtonDown("Fire2") && hugCDCounter <= 0 && patCDCounter <= 0)
                {
                    //gameObject.transform.forward = Vector3.ProjectOnPlane(CameraController.instance.transform.forward, Vector3.up).normalized;
                    hugCDCounter = hugCD;
                    animator.SetTrigger("hug");
                    hugStunCounter = hugStun;
                }
            }

            //Animator
            animator.SetFloat("move", move.sqrMagnitude);
            animator.SetFloat("yVelocity", characterController.velocity.y / 4);
            animator.SetBool("grounded", grounded2);
        }

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
        if (patStunCounter > 0)
        {
            patStunCounter -= Time.deltaTime;
        }
        if (hugStunCounter > 0)
        {
            hugStunCounter -= Time.deltaTime;
        }
        if (patDashDurationCounter > 0)
        {
            characterController.Move(transform.forward * Time.deltaTime * patDashForce);

            patDashDurationCounter -= Time.deltaTime;
        }
        if (jumpDashDurationCounter > 0)
        {
            characterController.Move(transform.forward * Time.deltaTime * jumpDashForce);

            jumpDashDurationCounter -= Time.deltaTime;
        }
    }
}
