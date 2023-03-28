using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    //initialize vars
    public int nrOfSpawns;
    public GameObject enemies;
    public bool isTestRoom = false;
    public Transform[] spawns;

    // Start is called before the first frame update
    void Start()
    {
            nrOfSpawns = gameObject.transform.childCount;
            spawns = new Transform[nrOfSpawns];
            //put spawnpoint transforms into array
            for (int i = 0; i < nrOfSpawns; i++)
            {
                spawns[i] = gameObject.transform.GetChild(i).transform;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(Transform point)
    {
        //pick a random enemy
        int enemyID = Random.Range(0, enemies.transform.childCount);
        //spawn enemy at a point
        Instantiate(enemies.transform.GetChild(enemyID), point);
    }
}
