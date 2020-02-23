using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonMovement : MonoBehaviour
{
    public bool isDead = false;
    public bool isBonusActive = false;
    bool isFlashing = true;
    int flashCount = 0;
    [SerializeField] GameObject bullet1, bullet2;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] Image flashImage;
    

    private void Start()
    {
        flashImage.canvasRenderer.SetAlpha(0.0f);
        isFlashing = true;
    }

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
        if (collision.gameObject.tag == "Meteor") { ProcessDeath(); }
        if (collision.gameObject.tag == "Bonus")
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
        ParticleSystem deathParticle = Instantiate(deathFX, transform.position, Quaternion.identity);
        Transform[] deathParticles = deathParticle.GetComponentsInChildren<Transform>();
        foreach (Transform particle in deathParticles) { particle.localScale = new Vector3(0.4f, 0.4f, 0.4f); }
        Destroy(deathParticle.gameObject, deathParticle.main.duration);
        flashImage.CrossFadeAlpha(1f, 0.2f, false);
        StartCoroutine(FlashEffect());
    }

    private IEnumerator FlashEffect()
    {
        while (isFlashing)
        {
            flashCount++;
            if (flashCount > 12) { isFlashing = false; }
            yield return null;
        }
        flashImage.CrossFadeAlpha(0.0f, 0.2f, false);
        Destroy(gameObject);
    }

}
