using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource dead;
    void Start()
    {
        if (gameObject.GetComponent<AudioSource>())
        {
            dead = transform.GetComponent<AudioSource>();
            dead.PlayOneShot(dead.clip);
        }
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
