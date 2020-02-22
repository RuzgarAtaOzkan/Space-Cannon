using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{

    [SerializeField] float meteorHealth = 150f;
    [SerializeField] ParticleSystem particleBlowFX;

    public void OnParticleCollision(GameObject other)
    {
        var particleToPlay = Instantiate(particleBlowFX, transform.position + Vector3.up * - 2f, Quaternion.identity);
        Destroy(particleToPlay.gameObject, 0.5f);

        meteorHealth -= 6f;
        if (meteorHealth < 0) { Destroy(gameObject); }
    }
}
