using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjController : MonoBehaviour
{
    public float damage = 20f;
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
        if (collision.transform.name.Contains("Player"))
        {
            PlrController player;
            player = collision.gameObject.GetComponent<PlrController>();
            player.TakeDamage(damage);
        }
        else
        {
            
        }
    }
}
