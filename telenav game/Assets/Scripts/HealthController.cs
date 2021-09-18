using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxLove;
    private int currentLove;

    public bool dead;

    private bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        currentLove = 0;

        if (GetComponent<PlayerController>() != null)
        {
            isPlayer = true;
        }

        if (isPlayer)
        {
            CanvasController.instance.UpdateHPDisplay((float)currentLove / maxLove);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetDamaged(15);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GetLoved(15);
        }
    }

    public void GetDamaged(int value)
    {
        if (!dead)
        {
            currentLove -= value;
            if (currentLove < 0)
            {
                currentLove = 0;
            }
        }
        if (isPlayer)
        {
            CanvasController.instance.UpdateHPDisplay((float)currentLove / maxLove);
        }
    }

    public void GetLoved(int value)
    {
        if (!dead)
        {
            currentLove += value;
            if (currentLove > maxLove)
            {
                currentLove = maxLove;
            }
        }
        if (isPlayer)
        {
            CanvasController.instance.UpdateHPDisplay((float)currentLove / maxLove);
        }
    }
}
