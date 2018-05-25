using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Quaternion currentRotation;

	void Update ()
    {
        currentRotation = transform.rotation;

        if (Input.GetAxis("RightHorizontal") != 0 || Input.GetAxis("RightVertical") != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-Input.GetAxis("RightHorizontal"), -Input.GetAxis("RightVertical")) * Mathf.Rad2Deg);
        }
        else
        {
            transform.rotation = currentRotation;
        }        
    }
}
