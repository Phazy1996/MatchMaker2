using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    
    [SerializeField]
    private Text _countDownText;
    [SerializeField]
    private float timerCount;


    void Start()
    {
        //starts with counting down.
        StartCoroutine(CountingDown());
    }
    private void UpdateText()
    {
        //calculates the minutes, seconds and fractions of the timercount.
        var minutes = timerCount / 60;
        var seconds = timerCount % 60;
        var fraction = (timerCount * 100) % 100;

        //updates the text to rounds the variables into 2 numbers.
        _countDownText.text = string.Format("{0:00}:{1:00}:{2:00}", Mathf.Floor(minutes), seconds, fraction); 
    }
    private IEnumerator CountingDown()
    {
        //as long the timercount hasn't reached zero.
        while(timerCount > 0)
        {
            //update the text.
            UpdateText();
            //lowers the timecount with the seconds.
            timerCount -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        //when time is up.
        _countDownText.text = "Time left: 00:00:00";
        EventManager.TimeIsUp();
    }
}