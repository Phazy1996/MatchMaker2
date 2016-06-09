using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //UI Buttons Main Menu
    [SerializeField]
    private GameObject howToPlayButton;
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private GameObject quitButton;
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject creditsButton;

    //UI Text
    [SerializeField]
    private GameObject gameTitle;
    [SerializeField]
    private GameObject howToPlayText;
    [SerializeField]
    private GameObject howToPlayTitle;
    [SerializeField]
    private GameObject creditsTitle;
    [SerializeField]
    private GameObject creditsText;


    void Start()
    {
        backButton.SetActive(false);
        howToPlayTitle.SetActive(false);
        howToPlayText.SetActive(false);
        creditsText.SetActive(false);
        creditsTitle.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void HowToPlayButton()
    {
        backButton.SetActive(true);
        playButton.SetActive(false);
        quitButton.SetActive(false);
        howToPlayButton.SetActive(false);
        creditsButton.SetActive(false);
        gameTitle.SetActive(false);
        howToPlayText.SetActive(true);
        howToPlayTitle.SetActive(true);
        creditsText.SetActive(false);
        creditsTitle.SetActive(false);
    }

    public void CreditsButton()
    {
        backButton.SetActive(true);
        playButton.SetActive(false);
        quitButton.SetActive(false);
        howToPlayButton.SetActive(false);
        creditsButton.SetActive(false);
        gameTitle.SetActive(false);
        howToPlayText.SetActive(false);
        creditsText.SetActive(true);
        creditsTitle.SetActive(true);
    }

    public void BackToMenuButton()
    {
        backButton.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
        howToPlayButton.SetActive(true);
        creditsButton.SetActive(true);
        gameTitle.SetActive(true);
        howToPlayText.SetActive(false);
        howToPlayTitle.SetActive(false);
        creditsText.SetActive(false);
        creditsTitle.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}