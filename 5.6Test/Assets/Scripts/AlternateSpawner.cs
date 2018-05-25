using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateSpawner : MonoBehaviour {

    [SerializeField]
    private int point;

    [SerializeField]
    private Vector3[] initialPoints, updateValues;
    private Vector3 currentUpdateValue;

    [SerializeField]
    private GameObject[] enemyTypes;

    [SerializeField]
    private int enemyType, numberOfEnemies;

    [SerializeField]
    private bool canSpawn = false;

    void Start ()
    {
        //StartCoroutine(SpawnLine());
	}
	
	void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnLine();
        }
    }

    void SpawnLine()
    {
        point = Random.Range(0, initialPoints.Length);
        //point = 0;
        switch(point)
        {
            case 1:
                transform.position = initialPoints[point];
                currentUpdateValue = updateValues[point];
                break;
            case 2:
                transform.position = initialPoints[point];
                currentUpdateValue = updateValues[point];
                break;
            case 3:
                transform.position = initialPoints[point];
                currentUpdateValue = updateValues[point];
                break;
            case 4:
                transform.position = initialPoints[point];
                currentUpdateValue = updateValues[point];
                break;
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject obj = Instantiate(enemyTypes[0], transform.position, transform.rotation);
            transform.position += updateValues[point];
        }
    }
}
