using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public static CameraScript script;

    [SerializeField]
    private GameObject player;

    private Vector3 pos;

    void Start()
    {
        script = this;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            CameraMovement();
        }        
    }

    void CameraMovement()
    {        
        transform.position = pos;
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
        pos = new Vector3(Mathf.Clamp(player.transform.position.x, -13, 13), Mathf.Clamp(player.transform.position.y, -23, 23), -10);
    }

    public void Shake(float amplitude, float duration, float dampStartPercentage = .75f)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCamera(amplitude, duration, dampStartPercentage));
        if (duration <= 0)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
        }
    }

    private IEnumerator ShakeCamera(float amplitude, float duration, float dampStartPercentage)
    {
        dampStartPercentage = Mathf.Clamp(dampStartPercentage, 0.0f, 1.0f);

        float elapsedTime = 0.0f;
        float damp = 1.0f;

        Vector3 cameraOrigin = transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float percentageComplete = elapsedTime / duration;

            if (percentageComplete >= dampStartPercentage && percentageComplete <= 1.0f)
            {
                damp = 1.0f - percentageComplete;
            }

            Vector2 offsetValues = Random.insideUnitCircle;

            offsetValues *= amplitude * damp;

            transform.position = new Vector3(transform.position.x + offsetValues.x, transform.position.y + offsetValues.y, transform.position.z);

            yield return null;
        }
    }
}
