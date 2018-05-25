using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject impact;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    void Update ()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Player"))
        {
            GameObject obj = Instantiate(impact, transform.position, transform.rotation);
            Destroy(obj, 2f);
            Destroy(gameObject);
        }
    }
}
