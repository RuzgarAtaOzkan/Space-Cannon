using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{

    [SerializeField] ParticleSystem particleBlowFX;
    [SerializeField] Meteor meteor;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnParticleCollision(GameObject other)
    {
        var particleToPlay = Instantiate(particleBlowFX, transform.position + Vector3.up * - 2f, Quaternion.identity);
        Destroy(particleToPlay.gameObject, 0.5f);

        meteor.meteorHealth -= 6f;
        if (meteor.meteorHealth < 0) { Destroy(gameObject); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            rb.AddForce(Vector3.up * 650f);
        }
    }
}
