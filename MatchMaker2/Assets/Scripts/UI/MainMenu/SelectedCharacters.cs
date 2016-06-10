using UnityEngine;
using System.Collections;

public class SelectedCharacters : MonoBehaviour {

    [SerializeField]
    private int playerNumber;

    private int characterCount = 1;

    private bool activated = false;

    [SerializeField]
    private int characterAmount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeCharacterCount(int value)
    {
        characterCount += value;

        if (characterCount > characterAmount)
        {
            characterCount = 1;
        }
        else if (characterCount < 1)
        {
            characterCount = characterAmount;
        }

        Debug.Log(characterCount + "For player:" + playerNumber);
    }
}
