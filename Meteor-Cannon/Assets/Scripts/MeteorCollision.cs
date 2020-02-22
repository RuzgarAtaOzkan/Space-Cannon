using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem particleBlowFX;
    [SerializeField] Meteor meteor;
    [SerializeField] MeteorSpawner meteorSpawner;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        meteorSpawner = FindObjectOfType<MeteorSpawner>();
        rb.AddForce(Vector3.right * 100f);
    }

    private void Update()
    {
        MoveIfEdge();
    }

    private void MoveIfEdge()
    {
        if (transform.position.x < -4f) { rb.AddForce(Vector3.right * 70f); }
        if (transform.position.x > 4) { rb.AddForce(Vector3.right * -70f); }
    }

    public void OnParticleCollision(GameObject other)
    {
        ProcessParticles();

        meteor.meteorHealth -= 4f;
        if (meteor.meteorHealth < 0)
        {
            meteor.DivideMeteor();
            Destroy(gameObject);
            meteorSpawner.meteorQueue.Dequeue();
        }
    }

    private void ProcessParticles()
    {
        var particleToPlay = Instantiate(particleBlowFX, transform.position + Vector3.up * -2f, Quaternion.identity);
        Destroy(particleToPlay.gameObject, 0.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube" || collision.gameObject.name == gameObject.name)
        {
            rb.AddForce(Vector3.up * 650f);
            rb.AddForce(Vector3.left * 30f);
        }
    }
}
