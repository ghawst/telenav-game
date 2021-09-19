using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.heartUnlocked = true;
        Destroy(gameObject, .3f);
    }
}
