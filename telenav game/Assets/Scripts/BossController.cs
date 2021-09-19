using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator animator;

    public int curentDest;
    public GameObject[] destinations;

    public float distanceFromPlayerToStart;
    public float distanceFromPlayerToMove;
    public float leaveForDestDelay;

    public float attackCD;
    private float attackCDCounter;

    public enum State { idle, waitingToLeaveForDest, goingToDest, attacking }
    public State state = State.idle;

    public GameObject attack;
    public GameObject pacifiedModel;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCDCounter = attackCD;

        curentDest = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            agent.SetDestination(destinations[curentDest++].transform.position);
            state = State.goingToDest;
            if (curentDest >= destinations.Length)
            {
                curentDest = 0;
            }
        }

        switch (state)
        {
            case State.idle:
                if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= distanceFromPlayerToStart)
                {
                    Move();
                }
                break;
            case State.waitingToLeaveForDest:
                
                break;
            case State.goingToDest:
                if (Vector3.Distance(transform.position, destinations[curentDest - 1 < 0 ? destinations.Length - 1 : curentDest - 1].transform.position) <= 4f)
                {
                    state = State.attacking;
                }
                break;
            case State.attacking:
                transform.LookAt(new Vector3(PlayerController.instance.transform.position.x, transform.position.y, PlayerController.instance.transform.position.z));
                if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= distanceFromPlayerToMove)
                {
                    state = State.waitingToLeaveForDest;
                    StartCoroutine(MoveWithDelayCo());
                }

                if (attackCDCounter > 0)
                {
                    attackCDCounter -= Time.deltaTime;
                }
                else
                {
                    attackCDCounter = attackCD;
                    var attackObj = Instantiate(attack);
                    animator.SetTrigger("attack");
                    attackObj.transform.position = transform.position + Vector3.up * 3;
                }
                break;
        }

        animator.SetFloat("move", agent.velocity.sqrMagnitude);
    }

    public void Move()
    {
        state = State.goingToDest;
        if (curentDest >= destinations.Length)
        {
            curentDest = 0;
        }
        agent.SetDestination(destinations[curentDest++].transform.position);
    }

    private IEnumerator MoveWithDelayCo()
    {
        yield return new WaitForSeconds(leaveForDestDelay);

        Move();
    }

    public void Pacified()
    {
        var model = Instantiate(pacifiedModel);
        model.transform.position = transform.position;
        model.transform.LookAt(new Vector3(PlayerController.instance.transform.position.x, model.transform.position.y, PlayerController.instance.transform.position.z));
    }
}
