using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;
    public float stunDuration;

    public EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthController>() != null)
        {
            other.GetComponent<HealthController>().GetDamaged(damage);
        }
        if (other.GetComponent<PlayerController>() != null && stunDuration > 0)
        {
            other.GetComponent<PlayerController>().GetStunned(stunDuration);
        }
        if (enemyController != null && enemyController.type.Equals(EnemyController.Type.sheep))
        {
            enemyController.StopDash();
        }
    }
}
