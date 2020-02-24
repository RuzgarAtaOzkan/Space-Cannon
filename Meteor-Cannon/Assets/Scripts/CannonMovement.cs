using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//finished the most parts

public class CannonMovement : MonoBehaviour
{
    public bool isDead = false;
    public bool isBonusActive = false;
    bool isFlashing = true;
    [SerializeField] GameObject bullet1, bullet2;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] Image flashImage;
    [SerializeField] GameObject meteorSpawner;
    [SerializeField] ManageGame manageGame;
    [SerializeField] public bool startGame;
    Touch touch;

    private void Start()
    {
        isDead = false;
        manageGame = FindObjectOfType<ManageGame>();
        flashImage.canvasRenderer.SetAlpha(0.0f);
        isFlashing = true;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out hit)) { transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z); }
        if (transform.position.x > 4f) { transform.position = new Vector3(4f, transform.position.y, transform.position.z); }
        if (transform.position.x < -4f) { transform.position = new Vector3(-4f, transform.position.y, transform.position.z); }
        if (startGame)
        {
            meteorSpawner.SetActive(true);
            startGame = false;
        }
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
        StartCoroutine(FlashEffect());
        StartCoroutine(AfterDeath());
    }

    private IEnumerator FlashEffect()
    {
        while (isFlashing)
        {
            flashImage.CrossFadeAlpha(1f, 0.2f, false);
            isFlashing = false;
            yield return new WaitForSeconds(0.12f);
        }
        flashImage.CrossFadeAlpha(0.0f, 0.2f, false);
    }

    IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.1f;
        manageGame.playButton.gameObject.SetActive(true);
        manageGame.isReloadable = true;
        Destroy(gameObject);
    }

}
