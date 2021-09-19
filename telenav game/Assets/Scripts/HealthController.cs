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

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PlayerController>() != null)
        {
            isPlayer = true;
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
            if (currentLove >= maxLove && !isPlayer)
            {
                Destroy(gameObject);
            }
        }
        canvasController.UpdateHPDisplay((float)currentLove / maxLove);
    }
}
