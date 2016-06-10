using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
public class ResultHandeler : MonoBehaviour {

    [SerializeField]
    private Text playerNameText;
    [SerializeField]
    private Text killNumbersText;
    [SerializeField]
    private GameObject resultScreen;
    private List<Movement> characters = new List<Movement>();
    [SerializeField]
    private Text introCountDownText;
	void Start () {
        EventManager.OnTimesUp += MakeResultScreen;
        GameObject[] tempplayers = GameObject.FindGameObjectsWithTag(Tags.player);
        for (int i = 0; i< tempplayers.Length; i ++)
        {
            characters.Add(tempplayers[i].GetComponent<Movement>());
        }
        StartCoroutine(Intro());
	}
    IEnumerator Intro()
    {
        foreach (Movement move in characters)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("sup");
            move.Respawn();
        }
        yield return new WaitForSeconds(1f);
        for (int i = 3; i > 0; i--)
        {
            introCountDownText.text = i.ToString();
            yield return new WaitForSeconds(0.5f);
        }
        introCountDownText.text = "Fight!";
        foreach (Movement move in characters)
        {
            move.InControl = true;
        }
        EventManager.StartCounting();
        yield return new WaitForSeconds(0.5f);
        introCountDownText.text = "";
    }
    void MakeResultScreen()
    {
        StartCoroutine(MakingResultScreen());
    }
    IEnumerator MakingResultScreen()
    {
        characters = characters.OrderBy(x => x.GetComponent<Movement>().KillCount * -2).ToList();
        yield return new WaitForSeconds(0.5f);
        resultScreen.SetActive(true);
        int place = 1;
        foreach (Movement move in characters)
        {
            playerNameText.text += place.ToString() + ". player " + move.PlayerId.ToString() + "\n";
            place++;
            killNumbersText.text += move.KillCount.ToString() + " kills \n";
            if (characters.IndexOf(move) != 0)
            {
                move.IsAlive = false;
                move.InControl = false;
            }
        }
    }
}
