using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public float healValue = 15f;
    private bool consumed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Contains("Player") && !consumed)
        {
            PlrController player = collision.gameObject.GetComponent<PlrController>();
            if (player.health < player.maxHealth && player.maxHealth - player.health >= healValue)
            {
                player.health += healValue;
                consumed = true;
            }
            else if (player.health < player.maxHealth && player.maxHealth - player.health < healValue)
            {
                player.health = player.maxHealth;
                consumed = true;
            }
            Destroy(gameObject);
        }
    }
}
