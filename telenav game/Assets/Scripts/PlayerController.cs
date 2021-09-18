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
    public float realStun;

    public int patLove;
    public int hugLove;
    public float loveBoxDuration;
    public GameObject loveBox;
    private Coroutine loveCo;

    private GameObject closestEnemy;

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

        if (GameManager.instance.enemies.Length > 0)
        {
            closestEnemy = GameManager.instance.enemies[0];
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

        if (!stunned && realStun <= 0)
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
                        if (playerVelocity.y > 0)
                        {
                            playerVelocity.y = -playerVelocity.y;
                        }
                        playerVelocity.y -= Mathf.Sqrt(stompForce * -0.01f * gravityValue);
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

            characterController.Move(playerVelocity * Time.deltaTime);

            //Attacks
            if (grounded2)
            {
                //Debug.Log(Mathf.Abs(Vector3.Distance(transform.position, closestEnemy.transform.position - Vector3.up * closestEnemy.transform.position.y)));
                if (Input.GetButtonDown("Fire1") && patCDCounter <= 0 && hugCDCounter <= 0)
                {
                    //gameObject.transform.forward = Vector3.ProjectOnPlane(CameraController.instance.transform.forward, Vector3.up).normalized;
                    if (closestEnemy != null && Mathf.Abs(Vector3.Distance(transform.position, closestEnemy.transform.position - Vector3.up * closestEnemy.transform.position.y)) < 7)
                    {
                        transform.LookAt(new Vector3(closestEnemy.transform.position.x, transform.position.y, closestEnemy.transform.position.z));
                    }
                    patCDCounter = patCD;
                    animator.SetTrigger("pat");
                    patDashDurationCounter = patDashDuration;
                    patStunCounter = patStun;
                    if (loveCo != null)
                    {
                        StopCoroutine(loveCo);
                    }
                    loveCo = StartCoroutine(LoveCo(patLove));
                }
                if (Input.GetButtonDown("Fire2") && hugCDCounter <= 0 && patCDCounter <= 0)
                {
                    //gameObject.transform.forward = Vector3.ProjectOnPlane(CameraController.instance.transform.forward, Vector3.up).normalized;
                    if (closestEnemy != null && Mathf.Abs(Vector3.Distance(transform.position, closestEnemy.transform.position - Vector3.up * closestEnemy.transform.position.y)) < 7 && closestEnemy != null)
                    {
                        transform.LookAt(new Vector3(closestEnemy.transform.position.x, transform.position.y, closestEnemy.transform.position.z));
                    }
                    hugCDCounter = hugCD;
                    animator.SetTrigger("hug");
                    hugStunCounter = hugStun;
                    if (loveCo != null)
                    {
                        StopCoroutine(loveCo);
                    }
                    loveCo = StartCoroutine(LoveCo(hugLove));
                }
            }
            playerVelocity.y += gravityValue * Time.deltaTime;

            //Animator
            animator.SetFloat("move", move.sqrMagnitude);
            animator.SetFloat("yVelocity", characterController.velocity.y / 4);
            animator.SetBool("grounded", grounded2);
        }
        animator.SetFloat("realStun", realStun);

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
        if (realStun > 0)
        {
            realStun -= Time.deltaTime;
        }

        FindClosestEnemy();
    }

    public void FindClosestEnemy()
    {
        if (GameManager.instance.enemies.Length > 0)
        {
            foreach (GameObject enemy in GameManager.instance.enemies)
            {
                if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position - Vector3.up * enemy.transform.position.y) < Vector3.Distance(transform.position, closestEnemy.transform.position - Vector3.up * closestEnemy.transform.position.y))
                {
                    closestEnemy = enemy;
                }
            }
        }
    }

    public void GetStunned(float duration)
    {
        realStun = duration;

        jumpDashDurationCounter = .001f;
    }

    public IEnumerator LoveCo(int love)
    {
        yield return new WaitForSeconds(.1f);

        loveBox.SetActive(true);
        loveBox.GetComponent<LoveDealer>().love = love;

        yield return new WaitForSeconds(loveBoxDuration);

        loveBox.SetActive(false);
    }
}
