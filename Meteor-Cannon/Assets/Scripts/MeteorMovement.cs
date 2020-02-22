using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    float pushing = 9f;
    float rotation;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, pushing, transform.position.z);
        transform.rotation = Quaternion.Euler(rotation += -1f, Quaternion.identity.y, Quaternion.identity.z);
        pushing -= 0.02f;
    }
}
