﻿using UnityEngine;
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
    [SerializeField]
    private List<Character> characters;
    [SerializeField]
    private Text introCountDownText;

    [SerializeField]
    private AudioSource fightGoingSound;
    [SerializeField]
    private AudioSource backgroundSound;
    [SerializeField]
    private AudioSource tickingSound;
    void Start () {
        characters = new List<Character>(0);
        EventManager.OnTimesUp += MakeResultScreen;
        GameObject[] tempplayers = GameObject.FindGameObjectsWithTag(Tags.player);
        for (int i = 0; i< tempplayers.Length; i ++)
        {
            if(tempplayers[i].name != "character1(Clone)" && tempplayers[i].name != "character2(Clone)")
                characters.Add(tempplayers[i].GetComponent<Character>());
        }
        StartCoroutine(Intro());
	}
    IEnumerator Intro()
    {
        backgroundSound.volume = 0.3f;
        yield return new WaitForSeconds(1f);
        foreach (Character move in characters)
        {
            yield return new WaitForSeconds(1f);
            move.Respawn();
        }
        yield return new WaitForSeconds(1f);
        backgroundSound.volume = 0.7f;
        for (int i = 3; i > 0; i--)
        {
            tickingSound.Play();
            introCountDownText.text = i.ToString();
            yield return new WaitForSeconds(0.5f);
        }
        tickingSound.Stop();
        introCountDownText.text = "Fight!";
        fightGoingSound.Play();
        foreach (Character move in characters)
        {
            move.InControl = true;
            move.gameObject.GetComponent<PlayerInput>().enabled = true;
        }
        EventManager.StartCounting();
        yield return new WaitForSeconds(0.5f);
        backgroundSound.volume = 1f;
        introCountDownText.text = "";
    }
    void MakeResultScreen()
    {
        StartCoroutine(MakingResultScreen());
    }
    IEnumerator MakingResultScreen()
    {
        fightGoingSound.Play();
        backgroundSound.Stop();
        characters = characters.OrderBy(x => x.GetComponent<Character>().KillCount * -2).ToList();
        yield return new WaitForSeconds(2f);
        resultScreen.SetActive(true);
        int place = 1;
        foreach (Character move in characters)
        {
            playerNameText.text += place.ToString() + ". player " + move.gameObject.GetComponent<PlayerInput>().PlayerId.ToString() + "\n";
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
