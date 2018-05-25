using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public static Bullet script;
    private AudioSource source;

    [SerializeField]
    private GameObject player;
    private Transform firePoint;

    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private GameObject impact;

    [SerializeField]
    private float speed, frequency, magnitude;

    [SerializeField]
    public int damage;
	
    void Start()
    {
        script = this;
        firePoint = GetComponentInChildren<Transform>();
        source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.pitch = Random.Range(.7f, 1.3f);
        }   
        GameObject muzzleObj = Instantiate(muzzleFlash, new Vector3(firePoint.position.x, firePoint.position.y, -1), firePoint.rotation);
        Destroy(muzzleObj, .1f);
    }

	void Update ()
    {
        if (FirePatterns.script.pattern == FirePatterns.Pattern.SineWave)
        {
            transform.Translate(player.transform.up * speed * Time.deltaTime);
            transform.position = transform.position + transform.right * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else
        {
            transform.Translate(player.transform.up * speed * Time.deltaTime);
        }
        Destroy(gameObject, 3f);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("Portal") || coll.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(impact, transform.position, transform.rotation);
            Destroy(obj, .2f);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
