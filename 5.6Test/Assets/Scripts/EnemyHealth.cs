using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [Header("Health")]
    [SerializeField]
    private int totalHealth;
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private GameObject deathParticle;

    [Header("Drops")]
    [SerializeField]
    private GameObject[] drops;
    [SerializeField]
    private int maxNumberOfDrops, numberOfDrops;
    private Vector2 force;

    [Header("Score")]    
    [SerializeField]
    private int scoreValue;

	void Start ()
    {
        currentHealth = totalHealth;	
	}
	
	void Update ()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        if (currentHealth <= 0)
        {
            //Drops
            numberOfDrops = Random.Range(0, maxNumberOfDrops);
            for (int i = 0; i < numberOfDrops; i++)
            {
                int dropVal = Random.Range(0, drops.Length);
                force = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                GameObject dropObj = Instantiate(drops[dropVal], transform.position, transform.rotation);
                dropObj.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
            DeadAnim();
        }
    }

    void DeadAnim()
    {
        //Death particles
        GameObject objDeadParticle = Instantiate(deathParticle, transform.position, transform.rotation);
        CameraScript.script.Shake(.2f, .2f);

        Score.script.AddScore(scoreValue);

        Destroy(objDeadParticle, 2f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet") || coll.gameObject.CompareTag("Homing"))
        {
            currentHealth -= Bullet.script.damage;
        }
        if (coll.gameObject.CompareTag("Special"))
        {
            currentHealth -= PlayerSpecial.script.damage;
        }

        if (coll.gameObject.CompareTag("Player") && gameObject.tag != "Portal" && gameObject.tag != "Boss")
        {
            DeadAnim();
        }
    }
}
