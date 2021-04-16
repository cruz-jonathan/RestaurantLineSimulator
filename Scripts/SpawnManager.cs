using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    //Sets the spawnPoints and sets up the list to utilize them
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;
    public GameObject spawnPoint5;
    public GameObject spawnPoint6;
    public GameObject spawnPoint7;
    public GameObject spawnPoint8;
    public GameObject spawnPoint9;
    public List<GameObject> spawn = new List<GameObject>();

    private List<GameObject> spawnFilter = new List<GameObject>();

    // Use this for initialization
    void Awake() {
        initializeSpawnPoints();
    }
    //Method that adds the spawnpoints to a list;
    void initializeSpawnPoints() {
        spawnFilter.Add(spawnPoint1);
        spawnFilter.Add(spawnPoint2);
        spawnFilter.Add(spawnPoint3);
        spawnFilter.Add(spawnPoint4);
        spawnFilter.Add(spawnPoint5);
        spawnFilter.Add(spawnPoint6);
        spawnFilter.Add(spawnPoint7);
        spawnFilter.Add(spawnPoint8);
        spawnFilter.Add(spawnPoint9);

        foreach (GameObject spawnPoint in spawnFilter)
        {
            if (spawnPoint)
            {
                spawn.Add(spawnPoint);
            }
        }
    }
}
