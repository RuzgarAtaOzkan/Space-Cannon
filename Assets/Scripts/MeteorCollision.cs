using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem particleBlowFX;
    [SerializeField] Meteor meteor;
    [SerializeField] MeteorSpawner meteorSpawner;
    [SerializeField] ManageGame manageGame;
    AudioSource audioSource;
    [SerializeField] AudioClip meteorGroundHitSFX;
    [SerializeField] AudioClip meteorSplitSFX;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        meteorSpawner = FindObjectOfType<MeteorSpawner>();
        manageGame = FindObjectOfType<ManageGame>();
    }

    private void Update()
    {
        MoveIfEdge();
    }

    private void MoveIfEdge()
    {
        if (transform.position.x < -4f) { rb.AddForce(Vector3.right * 30f); }
        if (transform.position.x > 4) { rb.AddForce(Vector3.right * -30f); }
    }

    public void OnParticleCollision(GameObject other)
    {
        ProcessParticles();
        meteor.meteorHealth -= 4f;
        manageGame.IncreasePoints();
        if (meteor.meteorHealth < 1)
        {
            meteor.DivideMeteor();
            Destroy(gameObject);
            if (meteorSpawner.meteorQueue.Count > 0) { meteorSpawner.meteorQueue.Dequeue(); } 
        }
    }

    private void ProcessParticles()
    {
        var particleToPlay = Instantiate(particleBlowFX, transform.position + Vector3.up * -2f, Quaternion.identity);
        Destroy(particleToPlay.gameObject, 0.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            rb.AddForce(Vector3.up * 750f);
            Handheld.Vibrate();
        }
        if (collision.gameObject.name == gameObject.name && collision.gameObject.GetComponent<Meteor>().isForceable)
        {
            rb.AddForce(Vector3.up * 750f);
        }
    }
    
}
