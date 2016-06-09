using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ResultHandeler : MonoBehaviour {

    [SerializeField]
    private List<Movement> characters = new List<Movement>();

	void Start () {
        GameObject[] tempplayers = GameObject.FindGameObjectsWithTag(Tags.player);
        for (int i = 0; i< tempplayers.Length; i ++)
        {
            characters.Add(tempplayers[i].GetComponent<Movement>());
        }
	}
	
    void CompareScore()
    {
        characters = characters.OrderBy(x => x.GetComponent<Movement>().KillCount).ToList();
        foreach(Movement move in characters)
        {
            Debug.Log(move.KillCount);
        }
    }
}
