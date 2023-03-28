using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //initialize vars

    public float maxHealth = 100f;
    public float health = 100f;
    public float damageOnContact = 25f;
    public float attackRate = 1f;

    public bool isDead = false;
    public ParticleSystem deathParticles;
    private Rigidbody rb;
    private NavMeshAgent agent;
    Material enemMat;
    private float attackDelay = 0f;
    Renderer rend;

    public AudioSource pain;

    //take damage function
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        pain.PlayOneShot(pain.clip);
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
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemMat = GetComponent<Renderer>().material;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();
        if (isDead)
        {
            rb.isKinematic = false;
            rb.freezeRotation = false;
            //enemMat.color = Color.red;
            //mess with procedural controller
            ProceduralGenController proc;
            proc = GameObject.Find("ProceduralGenerator").GetComponent<ProceduralGenController>();
            proc.enemiesRemaining -= 1;

            //particles woo
            Instantiate(deathParticles, gameObject.transform.position, gameObject.transform.rotation);


            //roll loot
            RollDrops();

            //remove from existence
            Destroy(gameObject);
        }
        else
        {
            agent.destination = GameObject.Find("Player").transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Contains("Player") && Time.time > attackDelay)
        {
            PlrController player = collision.gameObject.GetComponent<PlrController>();
            player.TakeDamage(damageOnContact);
            attackDelay = Time.time + (1 / attackRate);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.name.Contains("Player") && Time.time > attackDelay)
        {
            PlrController player = collision.gameObject.GetComponent<PlrController>();
            player.TakeDamage(damageOnContact);
            attackDelay = Time.time + (1 / attackRate);
        }
    }

    void RollDrops()
    {
        float roll = Random.Range(0f, 1f);
        //DEBUG
        print(roll);
        if(roll <= 0.05f)
        {
            Instantiate(GameObject.Find("drop_HealthPotion"), transform.position, transform.rotation);
        }
        if(roll <= 0.1f)
        {
            Instantiate(GameObject.Find("drop_Mushroom"), transform.position, transform.rotation);
        }
    }
}
