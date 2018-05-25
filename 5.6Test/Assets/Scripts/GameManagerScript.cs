using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    [Header("Level Info")]
    [SerializeField]
    private int levelNumber;

    [Header("Spawning Spawners")]
    [SerializeField]
    private GameObject[] spawners;
    private Vector2 spawnArea;

    [SerializeField]
    private GameObject[] activeEnemies;

    [SerializeField]
    private bool canSpawnPortal;
	
	void Update ()
    {
        if (canSpawnPortal)
        {
            StartCoroutine(SpawnPortal());
            canSpawnPortal = false;
        }

        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (activeEnemies.Length <= 0)
        {
            levelNumber += 1;
            canSpawnPortal = true;
        }
    }

    IEnumerator SpawnPortal()
    {
        for (int i = 0; i < levelNumber; i++)
        {
            spawnArea.x = Random.Range(-35, 35);
            spawnArea.y = Random.Range(-35, 35);
            GameObject portalObj = Instantiate(spawners[1], spawnArea, transform.rotation);
            yield return new WaitForSeconds(5f/levelNumber);
        }        
    }
}
