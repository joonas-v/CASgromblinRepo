using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGunController : MonoBehaviour
{
    //weapon attributes
    public float damage = 20f;
    public float firerate = 3f;
    public float impactForce = 5f;
    public float spread = 0.1f;
    public int wepID = 1;

    public ParticleSystem muzzleFlash;

    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
