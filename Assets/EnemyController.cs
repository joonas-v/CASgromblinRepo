using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //initialize vars

    public float maxHealth = 100f;
    public float health = 100f;

    private bool isDead = false;
    private Rigidbody rb;

    //take damage function
    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

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
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();
        if (isDead)
        {
            rb.isKinematic = false;
            rb.freezeRotation = false;
        }
        else
        {
            //do nothing
        }
    }
}
