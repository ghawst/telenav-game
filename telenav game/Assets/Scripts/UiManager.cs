using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject openCreditsButton, closeCreditButton; 

    public Animator uiAnimator;

    // Start is called before the first frame update
    void Start()
    {
        uiAnimator = gameObject.GetComponent<Animator>();
        openCreditsButton.SetActive(true);
        closeCreditButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            pauseMenuPanel.SetActive(true);
            GameManager.instance.UnhideCursor();
        }
    }

    public void HidePausePanel()
    {
        //PlayAnimation...
        //CoroutineForClosingPanel;
        pauseMenuPanel.SetActive(false);
        GameManager.instance.HideCursor();
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
        uiAnimator.SetTrigger("CreditsIn");
        closeCreditButton.SetActive(true);
        openCreditsButton.SetActive(false);
    }

    public void HideCredits()
    {
        uiAnimator.SetTrigger("CreditsOut");
        openCreditsButton.SetActive(true);
        closeCreditButton.SetActive(false);
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
