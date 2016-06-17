using UnityEngine;
using System.Collections;

public class SpawnPointData : MonoBehaviour {
    [SerializeField]
    private Transform[] spawnPoints;

    public Transform[] SpawnPoints
    {
        get { return spawnPoints; }
    }
}
