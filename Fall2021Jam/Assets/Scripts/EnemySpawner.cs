using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float xmin;
    [SerializeField] private float xmax;
    [SerializeField] private float ymin;
    [SerializeField] private float ymax;
    [SerializeField] private RoomEvent re; // debug

    // This value is used to constrain the spawn box a bit more; it makes it easy to input the x/y coords for the walls directly into min/max rather than doing subtraction
    [SerializeField] private float pad;

    // Relates to the spacing of the spawned enemies in SpawnFour. Not sure if this should be a constant here
    [SerializeField] private float offset;

    // Could do with making this an array later to choose random enemies. Depends on what we think about how clusters should spawn.
    [SerializeField] private GameObject toSpawn; 

    void SpawnCluster()
    {
        // this probably creates a more natural-looking cluster but kinda slower and harder:
        // define a bounding box.
        // spawn one dude in a random location (such that no part of the dude is outside the box)
        // spawn dude k in a place that does not overlap with dudes 1...k-1 and no part of the dude is outside the box
        // repeat until number of dudes spawned is wanted
    }

    void SpawnFour(GameObject toSpawn)
    {
        // choose a random point in the bounding box.
        // spawn 4 dudes; treat the chosen point as the center of the 4.

        Vector3 randomPoint = new Vector3(Random.Range(xmin + pad, xmax - pad), Random.Range(ymin + pad, ymax - pad), 0);
        GameObject go;
        go = Instantiate(toSpawn, randomPoint + new Vector3(offset, offset, 0), Quaternion.identity);
        go.GetComponent<Enemy>().SetRoomEvent(re);
        /*
        go = Instantiate(toSpawn, randomPoint + new Vector3(offset, -offset, 0), Quaternion.identity);
        go.GetComponent<Enemy>().SetRoomEvent(re);
        go = Instantiate(toSpawn, randomPoint + new Vector3(-offset, offset, 0), Quaternion.identity);
        go.GetComponent<Enemy>().SetRoomEvent(re);
        go = Instantiate(toSpawn, randomPoint + new Vector3(-offset, -offset, 0), Quaternion.identity);
        go.GetComponent<Enemy>().SetRoomEvent(re);
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) { SpawnFour(toSpawn); }
    }
}
