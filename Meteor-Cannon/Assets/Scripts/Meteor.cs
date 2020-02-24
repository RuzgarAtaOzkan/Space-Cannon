using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meteor : MonoBehaviour
{
    public float meteorHealth;

    public float firstMeteorHealth;
    public bool isForceable = true;
    [SerializeField] public bool isDivided = false;
    TextMesh textMesh;
    Rigidbody rb;
    
    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        textMesh.text = meteorHealth.ToString();
        ControlVelocities();
    }

    private void ControlVelocities()
    {
        if (rb.velocity.x > 6f) { rb.velocity = new Vector3(6f, rb.velocity.y, rb.velocity.z); }
        else if (rb.velocity.x < -6f) { rb.velocity = new Vector3(-6f, rb.velocity.y, rb.velocity.z); }
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
        var dividedMeteor1 = Instantiate(this, transform.position + Vector3.right, Quaternion.identity);
        var dividedMeteor2 = Instantiate(this, transform.position, Quaternion.identity);

        /*if (dividedMeteor1.transform.position.x < dividedMeteor2.transform.position.x)
        {
            dividedMeteor1.rb.velocity = new Vector3(0f, 0f, 0f);
            dividedMeteor2.rb.velocity = new Vector3(0f, 0f, 0f);
            dividedMeteor1.rb.AddForce(Vector3.right * -5f);
            dividedMeteor2.rb.AddForce(Vector3.right * 5f);
        } 
        else if (dividedMeteor1.transform.position.x > dividedMeteor2.transform.position.x)
        {
            dividedMeteor1.rb.velocity = new Vector3(0f, 0f, 0f);
            dividedMeteor2.rb.velocity = new Vector3(0f, 0f, 0f);
            dividedMeteor1.rb.AddForce(Vector3.right * 5f);
            dividedMeteor2.rb.AddForce(Vector3.right * -5f);
        }*/

        float dividedScale = transform.localScale.x / 2;
        if (dividedScale < 40f) { dividedScale = 50f; }
        dividedMeteor1.meteorHealth = Mathf.RoundToInt(firstMeteorHealth / 2);
        dividedMeteor2.meteorHealth = Mathf.RoundToInt(firstMeteorHealth / 2);
        dividedMeteor1.transform.localScale = new Vector3(dividedScale, dividedScale, dividedScale);
        dividedMeteor2.transform.localScale = new Vector3(dividedScale, dividedScale, dividedScale);
        dividedMeteor1.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        dividedMeteor2.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        dividedMeteor1.isForceable = false;
        dividedMeteor2.isForceable = false;
        dividedMeteor1.isDivided = true;
        dividedMeteor2.isDivided = true;
        isDivided = true;
    }
}
