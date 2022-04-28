using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHere : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject player;


    void Awake()//esimesena antakse muutujale, spawnkoht
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Start is called before the first frame update
    void Start()//leitakse karakteri prefab ning spawnitakse see
    {
        player = (GameObject)Resources.Load("player", typeof(GameObject));

        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlayer()//funktsioon karakteri spawnimiseks
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(player, spawnLocations[spawn].transform.position, Quaternion.identity);
    }
}
