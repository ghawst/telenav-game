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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            pauseMenuPanel.SetActive(true);
            uiAnimator.SetTrigger("PauseIn");
            GameManager.instance.UnhideCursor();
            GameManager.instance.paused = true;
        }
    }

    public void HidePausePanel()
    {
        uiAnimator.SetTrigger("PauseOut");
        StartCoroutine(HidePausePanelCo());
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
        SceneManager.LoadScene("Level01");
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator HidePausePanelCo()
    {
        yield return new WaitForSeconds(1f);
        pauseMenuPanel.SetActive(false);
        GameManager.instance.HideCursor();
        GameManager.instance.paused = false;
    }

}
