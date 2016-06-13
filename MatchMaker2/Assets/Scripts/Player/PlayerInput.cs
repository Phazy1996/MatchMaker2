using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    private Character character;
    [SerializeField]
    private int playerId = 1;

    void Start () {
        character = GetComponent<Character>();
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire1_P" + playerId.ToString()))
        {
            character.Shoot();
        }
        if (Input.GetButton("Jump_P" + playerId.ToString()))
        {
            character.Jump();
        }
        if (Input.GetButton("Down_P" + playerId.ToString()))
        {
            character.Down();
        }

        character.XSpeed = Input.GetAxis("Horizontal_P" + playerId.ToString());

    }
    public int PlayerId
    {
        get { return playerId; }
        set { playerId = value; }
    }

}
