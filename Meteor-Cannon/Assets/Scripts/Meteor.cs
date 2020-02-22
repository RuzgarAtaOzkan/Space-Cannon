using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float meteorHealth;
    public float firstMeteorHealth;
    TextMesh textMesh;
    Rigidbody rb;

    [SerializeField] public bool isDivided = false;

    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        textMesh.text = meteorHealth.ToString();
        if (transform.position.y < 3f)
        {
            rb.AddForce(Vector3.up * 600f);
        } 
    }

    public float RandomHealth()
    {
        meteorHealth = Random.Range(50f, 150f);
        firstMeteorHealth = meteorHealth;
        int finalValue = Mathf.RoundToInt(meteorHealth);
        return finalValue;
    }

    public void DivideMeteor()
    {
        if (isDivided) { return; }

        var dividedMeteor1 = Instantiate(this, transform.position, Quaternion.identity);
        var dividedMeteor2 = Instantiate(this, transform.position, Quaternion.identity);

        float dividedScale = transform.localScale.x / 2;

        dividedMeteor1.meteorHealth = Mathf.RoundToInt(firstMeteorHealth / 2);
        dividedMeteor2.meteorHealth = Mathf.RoundToInt(firstMeteorHealth / 2);
            
        dividedMeteor1.transform.localScale = new Vector3(dividedScale, dividedScale, dividedScale);
        dividedMeteor2.transform.localScale = new Vector3(dividedScale, dividedScale, dividedScale);

        dividedMeteor1.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        dividedMeteor2.transform.rotation = Quaternion.Euler(-90f, 0, 0);

        dividedMeteor1.GetComponent<Rigidbody>().AddForce(Vector3.right * 20f);
        dividedMeteor2.GetComponent<Rigidbody>().AddForce(Vector3.right * -20f);

        dividedMeteor1.isDivided = true;
        dividedMeteor2.isDivided = true;

        isDivided = true;
    }
}
