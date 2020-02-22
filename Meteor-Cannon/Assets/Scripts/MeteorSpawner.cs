using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] Meteor meteor;
    public Queue<Meteor> meteorQueue = new Queue<Meteor>();

    public float healthToScale;

    void Start()
    {
        StartCoroutine(SpawnMeteor());
    }    

    IEnumerator SpawnMeteor()
    {
        while (true)
        {
            SpawnLoop();
            yield return new WaitForSeconds(Random.Range(2.5f, 4f));
        }
    }

    private void SpawnLoop()
    {
        if (meteorQueue.Count > 0) { return; }
        Meteor meteorToAdd = Instantiate(meteor, new Vector3(Random.Range(4f, -4f), 21f, -4f), Quaternion.identity);
        meteorToAdd.meteorHealth = meteorToAdd.RandomHealth();
        healthToScale = meteorToAdd.meteorHealth;
        meteorToAdd.transform.localScale = new Vector3(healthToScale, healthToScale, healthToScale);
        meteorToAdd.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        meteorQueue.Enqueue(meteorToAdd);
    }
}
