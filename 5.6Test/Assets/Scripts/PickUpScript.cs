using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

    public static PickUpScript script;

    public enum Effect
    {
        Health,
        Special,
        Currency
    }
    public Effect effect;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
	
    void Start()
    {
        script = this;

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

	void FixedUpdate ()
    {
        StartCoroutine(Attraction());
	}

    IEnumerator Attraction()
    {
        yield return new WaitForSeconds(1f);
        if (target != null)
        {
            Vector2 dir = target.transform.position - transform.position;
            rb.AddForce(speed * dir);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
