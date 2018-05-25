using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffectScript : MonoBehaviour {

    public float speed;

    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + speed * Time.deltaTime, transform.localScale.y + speed * Time.deltaTime, 1);
    }
}
