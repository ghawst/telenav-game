using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] enemies;

    private void Awake()
    {
        instance = this;

        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    // Start is called before the first frame update
    void Start()
    {
        HideCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnhideCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
