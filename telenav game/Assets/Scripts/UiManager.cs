using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    public void HidePausePanel()
    {
        //PlayAnimation...
        //CoroutineForClosingPanel;
        pauseMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("UIBlockOut");
    }

    public void ShowCredits()
    {

    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("EnviroBlockOut");
    }


    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
