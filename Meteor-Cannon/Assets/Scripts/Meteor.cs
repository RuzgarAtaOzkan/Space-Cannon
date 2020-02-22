using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float meteorHealth;
    TextMesh textMesh;

    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {
        textMesh.text = meteorHealth.ToString();
    }

    public float RandomHealth()
    {
        meteorHealth = Random.Range(50f, 500f);
        return Mathf.RoundToInt(meteorHealth);
    }
}
