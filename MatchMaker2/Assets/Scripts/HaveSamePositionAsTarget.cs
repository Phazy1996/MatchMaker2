using UnityEngine;
using System.Collections;

public class HaveSamePositionAsTarget : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offSet = new Vector3(0,0,0);
    [SerializeField]
    private bool trappedInScreen = false;
	void Update () {
         transform.position = target.position + offSet;
        if(trappedInScreen)
        {
            Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        }
    }
}
