using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject[] lanePrefabs;
    public float spawnHorizon = 500f;
    public float laneWidth = 2;
    public GameObject player;
    public Transform spawnerParent;


    private float nextLaneOffset = 0;

    // Use this for initialization
    void Start ()
    {
    }
    
    // Update is called once per frame
    void Update ()
    {
        var forwardPosition = player.transform.position.z; 
        while (forwardPosition + spawnHorizon > nextLaneOffset)
        {
            var randomLaneIndex = Random.Range(0, lanePrefabs.Length);
            var lane = lanePrefabs[randomLaneIndex];
            var nextLanePosition = nextLaneOffset*Vector3.forward;

            var newLaneObject = Instantiate(lane, nextLanePosition, Quaternion.identity) as GameObject;
            newLaneObject.transform.parent = spawnerParent;
            nextLaneOffset += laneWidth;
        }
        

    }
}
