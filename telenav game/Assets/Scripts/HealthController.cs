using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public CanvasController canvasController;

    public int maxLove;
    private int currentLove;

    public float invulnerability;
    private float invulnerabilityCounter;

    public bool dead;

    private bool isPlayer = false;
    private bool isBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PlayerController>() != null)
        {
            isPlayer = true;
        }
        else if (GetComponent<BossController>() != null)
        {
            isBoss = true;
        }

            if (isPlayer)
        {
            currentLove = maxLove;
        }
        else
        {
            currentLove = 0;
        }

        canvasController.UpdateHPDisplay((float)currentLove / maxLove);
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerabilityCounter > 0)
        {
            invulnerabilityCounter -= Time.deltaTime;
        }
    }

    public void GetDamaged(int value)
    {
        if (!dead && invulnerabilityCounter <= 0)
        {
            invulnerabilityCounter = invulnerability;
            currentLove -= value;
            if (currentLove < 0)
            {
                currentLove = 0;
            }
        }
        if (currentLove <= 0 && isPlayer)
        {
            PlayerController.instance.Lose();
            dead = true;
        }
        canvasController.UpdateHPDisplay((float)currentLove / maxLove);
    }

    public void GetLoved(int value)
    {
        if (!dead && invulnerabilityCounter <= 0)
        {
            invulnerabilityCounter = invulnerability;
            currentLove += value;
            if (currentLove > maxLove)
            {
                currentLove = maxLove;
            }
            if (currentLove >= maxLove && !isPlayer && !isBoss)
            {
                GetComponent<EnemyController>().Pacified();
                dead = true;
                Destroy(gameObject);
            }
            if (currentLove >= maxLove && isBoss)
            {
                GetComponent<BossController>().Pacified();
                dead = true;
                Destroy(gameObject);
            }
        }
        canvasController.UpdateHPDisplay((float)currentLove / maxLove);
    }
}
