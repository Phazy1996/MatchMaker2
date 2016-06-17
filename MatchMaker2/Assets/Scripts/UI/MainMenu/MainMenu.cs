using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Splash Screen
    [SerializeField]
    private GameObject splashScreen;

    //UI Buttons Main Menu
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject mainMenuButtons;

    //UI Text
    [SerializeField]
    private GameObject gameTitle;
    [SerializeField]
    private GameObject howToPlay;
    [SerializeField]
    private GameObject credits;



    void Start()
    {
        if(!EventManager.started)
        {
            splashScreen.SetActive(true);
            EventManager.started = true;
        }
        else
        {
            splashScreen.SetActive(false);
        }
        backButton.SetActive(false);
        howToPlay.SetActive(false);
        credits.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
            splashScreen.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("CameraTest");
    }

    public void HowToPlayButton()
    {
        backButton.SetActive(true);
        mainMenuButtons.SetActive(false);
        gameTitle.SetActive(false);
        howToPlay.SetActive(true);
        credits.SetActive(false);
    }

    public void CreditsButton()
    {
        backButton.SetActive(true);
        mainMenuButtons.SetActive(false);
        gameTitle.SetActive(false);
        howToPlay.SetActive(false);
        credits.SetActive(true);
    }

    public void BackToMenuButton()
    {
        backButton.SetActive(false);
        mainMenuButtons.SetActive(true);
        gameTitle.SetActive(true);
        howToPlay.SetActive(false);
        credits.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}