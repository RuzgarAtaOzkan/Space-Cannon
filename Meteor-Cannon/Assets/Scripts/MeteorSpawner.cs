using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] Meteor meteor;
    [SerializeField] List<Meteor> meteors;

    void Start()
    {
        StartCoroutine(SpawnMeteor());
    }

    private void Update()
    {
        DestroyMeteors();
    }

    IEnumerator SpawnMeteor()
    {
        while (true)
        {
            Meteor meteorToAdd = Instantiate(meteor, new Vector3(Random.Range(4f, -4f), 21f, -4f), Quaternion.identity);
            meteorToAdd.meteorHealth = meteorToAdd.RandomHealth();
            meteorToAdd.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            meteors.Add(meteorToAdd);
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
        }
    }

    void DestroyMeteors()
    {
        foreach (Meteor meteor in meteors)
        {
            if (meteor != null)
            {
                if (meteor.transform.position.y < -5f) 
                {
                    meteors.Remove(meteor);
                    Destroy(meteor.gameObject); 
                }
            }
        }
    }
}
