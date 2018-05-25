using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonBoss : MonoBehaviour {

    [SerializeField]
    private Transform[] firePoints;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float fireRate, rotSpeed, xSpeed, ySpeed;

    bool canFire = true;
    bool rotate = true;
    bool moving = true;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (canFire)
        {
            StartCoroutine(RandomShooting());
            canFire = false;
        }

        if (rotate)
        {
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        }

        //if (moving)
        //{
        //    transform.Translate(new Vector2(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime));
        //}    
	}

    IEnumerator Fire()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject obj = Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
        }        
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    IEnumerator RandomShooting()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject obj = Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
            yield return new WaitForSeconds(fireRate);
        }
        canFire = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            xSpeed *= -1;
            ySpeed *= -1;
        }
    }
}
