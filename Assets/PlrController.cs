using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrController : MonoBehaviour
{
    //initialize vars
    public float maxHealth = 100f;
    public float health = 100f;
    public bool isDead = false;
    public GameObject deadText;
    public int healthPotions = 1;
    public float healthPotionHeal = 50f;

    private Rigidbody rb;
    private PlrMove movement;
    private AudioSource painSound;

    //take damage
    public void TakeDamage(float dmg)
    {
        if (!isDead)
        {
            health -= dmg;
            painSound.PlayOneShot(painSound.clip);
        }
    }
    //check if dead
    public void DeadCheck()
    {
        if (health <= 0)
        {
            isDead = true;
            health = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlrMove>();
        painSound = GetComponent<AudioSource>();
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
            deadText.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UseHealthPotion();
            }
        }

    }

    void UseHealthPotion()
    {
        if(health != maxHealth && healthPotions > 0)
        {
            float differenceFromMax = maxHealth - health;
            if(differenceFromMax > 50)
            {
                health += healthPotionHeal;
                healthPotions -= 1;
            }
            else
            {
                health = maxHealth;
                healthPotions -= 1;
            }
        }
    }
}
