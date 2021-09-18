using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum Type { sheep, duck, partyGuy }

    public Type type;

    private EnemyAi enemyAi;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyAi = GetComponent<EnemyAi>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("move", enemyAi.agent.velocity.sqrMagnitude);
    }
}
