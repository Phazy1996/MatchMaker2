using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    
    [SerializeField]
    private Text _countDownText;
    [SerializeField]
    private float timerCount;
    [SerializeField]
    private AudioSource lastSecondsSound;

    void Start()
    {
        UpdateText();
        EventManager.OnStartCounting += StartCounting;
    }
    private void UpdateText()
    {
        //calculates the minutes, seconds and fractions of the timercount.
        var minutes = timerCount / 60;
        var seconds = timerCount % 60;
        var fraction = (timerCount * 100) % 100;

        //updates the text to rounds the variables into 2 numbers.
        _countDownText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(minutes), seconds); 
    }
    private void StartCounting()
    {
        StartCoroutine(CountingDown());
    }
    private IEnumerator CountingDown()
    {

        //as long the timercount hasn't reached ten.
        while(timerCount > 10)
        {
            //update the text.
            UpdateText();
            //lowers the timecount with the seconds.
            timerCount -= 1;
            yield return new WaitForSeconds(1f);
        }

        _countDownText.fontSize = 50;
        _countDownText.color = Color.red;
        lastSecondsSound.Play();
        //as long the timercount hasn't reached zero.
        while (timerCount >= 0)
        {
            //update the text.
            UpdateText();
            //lowers the timecount with the seconds.
            timerCount -= 1;
            yield return new WaitForSeconds(1f);
        }

        //when time is up.
        EventManager.TimeIsUp();
        _countDownText.fontSize = 60;
        _countDownText.color = Color.white;
        _countDownText.text = "Time's up!";
    }
}