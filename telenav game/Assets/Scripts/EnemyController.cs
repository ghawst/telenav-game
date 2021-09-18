using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum Type { sheep, duck, partyGuy }

    public Type type;

    private EnemyAi enemyAi;
    private Animator animator;

    [Header("Sheep")]
    public float dashSpeed;
    public float dashDuration;
    private float dashDurationCounter;

    // Start is called before the first frame update
    void Start()
    {
        enemyAi = GetComponent<EnemyAi>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type.Equals(Type.sheep))
        {
            if (dashDurationCounter > 0)
            {
                dashDurationCounter -= Time.deltaTime;

                enemyAi.agent.Move(transform.forward * dashSpeed * Time.deltaTime);

                animator.SetBool("attacking", true);
                if (dashDurationCounter <= 0)
                {
                    enemyAi.agent.SetDestination(enemyAi.transform.position);
                    animator.SetBool("attacking", false);
                }
            }
            else
            {
                animator.SetFloat("move", enemyAi.agent.velocity.sqrMagnitude);
            }
        }
    }

    public void Attack()
    {
        if (type.Equals(Type.sheep))
        {
            dashDurationCounter = dashDuration;
        }
    }
}
