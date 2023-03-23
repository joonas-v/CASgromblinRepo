using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrController : MonoBehaviour
{
    //initialize vars
    public float maxHealth = 100f;
    public float health = 100f;
    public bool isDead = false;

    private Rigidbody rb;
    private PlrMove movement;

    //take damage
    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
    //check if dead
    public void DeadCheck()
    {
        if (health <= 0)
        {
            isDead = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlrMove>();
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();
        if (isDead)
        {
            rb.isKinematic = false;
            rb.freezeRotation = true;
            movement.canMove = false;
        }
        else
        {
            //do nothing
        }
    }
}
