using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour {

    public static PlayerSpecial script;

    [SerializeField]
    private GameObject endParticles;

    public int damage;
    private float size;
    private bool canGrow = true;

	void Start ()
    {
        script = this;
	}
	
	void Update ()
    {
        size += .1f;
        transform.localScale = new Vector3(size, size, size);

        if (canGrow)
        {
            StartCoroutine(Grow());
            canGrow = false;
        }
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(2f);
        GameObject endObj = Instantiate(endParticles, transform.position, transform.rotation);
        endObj.transform.localScale = new Vector3(size, size, size);
        Destroy(gameObject);
        Destroy(endObj, 2f);
    }
}
