using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] Meteor meteor;
    [SerializeField] BonusMove bonus;
    public Queue<Meteor> meteorQueue = new Queue<Meteor>();

    public float healthToScale;

    void Start()
    {
        StartCoroutine(SpawnMeteor());
        StartCoroutine(SpawnBonus());
    }

    IEnumerator SpawnBonus()
    {
        while (true)
        {
            BonusSpawnLoop();
            yield return new WaitForSeconds(UnityEngine.Random.Range(16f, 25f));
        }
    }

    private void BonusSpawnLoop()
    {
        Instantiate(bonus, new Vector3(UnityEngine.Random.Range(4f, -4f), 21f, -4f), Quaternion.identity);
    }

    IEnumerator SpawnMeteor()
    {
        while (true)
        {
            MeteorSpawnLoop();
            yield return new WaitForSeconds(UnityEngine.Random.Range(4f, 6f));
        }
    }

    private void MeteorSpawnLoop()
    {
        if (meteorQueue != null)
        {
            if (meteorQueue.Count > 0) { return; }
            Meteor meteorToAdd = Instantiate(meteor, new Vector3(UnityEngine.Random.Range(4f, -4f), 21f, -4f), Quaternion.identity);
            meteorToAdd.meteorHealth = meteorToAdd.RandomHealth();
            healthToScale = meteorToAdd.meteorHealth;
            meteorToAdd.transform.localScale = new Vector3(healthToScale, healthToScale, healthToScale);
            meteorToAdd.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            meteorQueue.Enqueue(meteorToAdd);
        }
    }
}
