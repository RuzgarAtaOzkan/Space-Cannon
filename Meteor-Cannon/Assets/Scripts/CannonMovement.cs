using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonMovement : MonoBehaviour
{
    public bool isDead = false;
    public bool isBonusActive = false;
    [SerializeField] GameObject bullet1, bullet2;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) { transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z); }
        if (transform.position.x > 4f) { transform.position = new Vector3(4f, transform.position.y, transform.position.z); }
        if (transform.position.x < -4f) { transform.position = new Vector3(-4f, transform.position.y, transform.position.z); }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            ProcessDeath();
        }
        else if (collision.gameObject.tag == "Bonus")
        {
            isBonusActive = true;
            StartCoroutine(ProcessBonusFire());
            
        }
    }

    private IEnumerator ProcessBonusFire()
    {
        while (isBonusActive)
        {
            bullet1.SetActive(isBonusActive);
            bullet2.SetActive(isBonusActive);
            isBonusActive = false;
            yield return new WaitForSeconds(7f);
        }
        bullet1.SetActive(isBonusActive);
        bullet2.SetActive(isBonusActive);
    }

    private void ProcessDeath()
    {
        isDead = true;
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float xRotation = 0f;
        float yRotation = 0f;
        bool isShaking = true;
        int count = 0;
        while (isShaking)
        {
            float magnitudeX = UnityEngine.Random.Range(-2f, 2f);
            float magnitudeY = UnityEngine.Random.Range(-2f, 2f);
            Camera.main.transform.rotation = Quaternion.Euler(xRotation += magnitudeX, yRotation += magnitudeY, transform.rotation.z);
            count++;
            if (count > 6) 
            { 
                isShaking = false;
                Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z);
            }
            yield return new WaitForSeconds(0.06f);
        }
    }
}
