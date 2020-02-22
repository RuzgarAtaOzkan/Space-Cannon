using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) { transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z); }
        if (transform.position.x > 4f) { transform.position = new Vector3(4f, transform.position.y, transform.position.z); }
        if (transform.position.x < -4f) { transform.position = new Vector3(-4f, transform.position.y, transform.position.z); }
    }
}
