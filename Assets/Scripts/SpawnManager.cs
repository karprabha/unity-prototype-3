using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float spawnDelay = 2.0f;
    private float spawnInterval = 2.0f;
    private PlayerController playerControllerScript;
    void Start(){
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle(){
        if(playerControllerScript.gameOver == false){
            int obstacleIndx = Random.Range(0,obstaclePrefab.Length);
            Instantiate(obstaclePrefab[obstacleIndx], spawnPos, obstaclePrefab[obstacleIndx].transform.rotation);
        }
    }
}
