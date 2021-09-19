using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum Type { sheep, duck, partyGuy }

    public Type type;

    private EnemyAi enemyAi;
    private Animator animator;

    public GameObject goodModel;

    [Header("Sheep")]
    public int damage;
    public float dashSpeed;
    public float dashDuration;
    private float dashDurationCounter;
    public GameObject damageDealer;

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
                    damageDealer.SetActive(false);
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
            damageDealer.SetActive(true);
        }
    }

    public void StopDash()
    {
        dashDurationCounter = .01f;
    }

    public void Pacified()
    {
        var model = Instantiate(goodModel);
        if (type.Equals(Type.sheep))
        {
            model.transform.position = transform.position + Vector3.up * -3.5f;
        }
        model.transform.LookAt(new Vector3(PlayerController.instance.transform.position.x, model.transform.position.y, PlayerController.instance.transform.position.z));
    }
}
