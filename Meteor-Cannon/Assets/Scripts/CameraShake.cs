using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CannonMovement cannonMovement;
    bool isShaking = true;
    int shakeCount = 0;

    void Start()
    {
        cannonMovement = FindObjectOfType<CannonMovement>();
    }

    void Update()
    {
        if (cannonMovement.isDead)
        {
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        float xRotation = 0f;
        float yRotation = 0f;
        while (isShaking)
        {
            float magnitudeX = UnityEngine.Random.Range(-2f, 2f);
            float magnitudeY = UnityEngine.Random.Range(-2f, 2f);
            transform.rotation = Quaternion.Euler(xRotation += magnitudeX, yRotation += magnitudeY, transform.rotation.z);
            shakeCount++;
            if (shakeCount > 120)
            {
                isShaking = false;
                transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z);
            }
            yield return null;
        }
    }
}
