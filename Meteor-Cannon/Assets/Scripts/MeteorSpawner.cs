using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] MeteorMovement meteor;
    [SerializeField] List<MeteorMovement> meteors;

    // Start is called before the first frame update
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
            MeteorMovement meteorToAdd = Instantiate(meteor, new Vector3(Random.Range(6f, -6f), 9f, -4f), Quaternion.identity);
            meteors.Add(meteorToAdd);
            yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
        }
    }

    void DestroyMeteors()
    {
        foreach (MeteorMovement meteor in meteors)
        {
            if (meteor != null)
            {
                if (meteor.transform.position.y < -2f) { Destroy(meteor.gameObject); }
            }
        }
    }
}
