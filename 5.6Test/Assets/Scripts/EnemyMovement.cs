using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private GameObject player;
    private Transform target;

    [SerializeField]
    private float speed, rotSpeed;

    public enum MovePattern
    {
        LinearRight,
        LinearDown,
        Homing,
        Circle
    };

    public MovePattern movePattern;
	
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //if (movePattern == MovePattern.Linear)
        //{
        //    transform.Rotate(0, 0, Random.Range(0, 360));
        //}
    }

	void Update ()
    {

        if (player != null)
        {
            switch (movePattern)
            {
                case MovePattern.LinearRight:
                    LinearRight();
                    break;
                case MovePattern.LinearDown:
                    LinearDown();
                    break;
                case MovePattern.Circle:
                    break;
                case MovePattern.Homing:
                    HomingMovement();
                    break;
            }
        }
    }

    void LinearRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void LinearDown()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void HomingMovement()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

        //Look at player + rotate
        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.x, diff.y) * -Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot_z);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            speed *= -1;
        }
    }
}
