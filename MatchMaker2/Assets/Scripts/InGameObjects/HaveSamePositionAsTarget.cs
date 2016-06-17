using UnityEngine;
using System.Collections;

public class HaveSamePositionAsTarget : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offSet = new Vector3(0,0,0);
	void Update () {
         transform.position = target.position + offSet;
    }
}
