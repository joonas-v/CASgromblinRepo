using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionScript : MonoBehaviour
{
    bool consumed = false;
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
        if (collision.transform.name.Contains("Player") && !consumed)
        {
            PlrController player = collision.gameObject.GetComponent<PlrController>();
            player.healthPotions += 1;
            consumed = true;
            Destroy(gameObject);
        }
    }
}
