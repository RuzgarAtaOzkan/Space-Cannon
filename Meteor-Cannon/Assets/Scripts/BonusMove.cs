using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMove : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] ParticleSystem bonusFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveIfEdge();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            rb.AddForce(Vector3.up * 650f);
            rb.AddForce(Vector3.left * 30f);
            Handheld.Vibrate();
        }
        if (collision.gameObject.tag == "Player")
        {
            var bonusParticle = Instantiate(bonusFX, transform.position, Quaternion.identity);
            Destroy(bonusParticle, bonusParticle.main.duration);
            Destroy(gameObject);
        }
    }

    private void MoveIfEdge()
    {
        if (transform.position.x < -4f) { rb.AddForce(Vector3.right * 50f); }
        if (transform.position.x > 4) { rb.AddForce(Vector3.right * -50f); }
    }
}
