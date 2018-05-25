using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemyTypes;

    [SerializeField]
    private GameObject rippleObject;

    [SerializeField]
    private int enemyType, numberOfEnemies;

    [SerializeField]
    private bool canSpawn = false;

    [SerializeField]
    private bool canSpawnInfinite = false;
    private bool quitting = false;
	
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

	void Update ()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawn());
            canSpawn = false;
        }

        if (canSpawnInfinite)
        {
            StartCoroutine(SpawnInfinite());
            canSpawnInfinite = false;
        }
	}

    IEnumerator Spawn()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyType = Random.Range(0, enemyTypes.Length);
            GameObject obj = Instantiate(enemyTypes[enemyType], transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpawnInfinite()
    {
        enemyType = Random.Range(0, enemyTypes.Length);
        GameObject obj = Instantiate(enemyTypes[enemyType], transform.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        canSpawnInfinite = true;
    }

    void OnApplicationQuit()
    {
        quitting = true;
    }

    void OnDestroy()
    {
        if (!quitting)
        {
            GameObject obj = Instantiate(rippleObject, transform.position, transform.rotation);
            Destroy(obj, 3f);
        }
    }
}
