using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxLove;
    private int currentLove;

    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        currentLove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
