using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepCont : MonoBehaviour
{
    //initializing vars
    public bool hitScan = true;
    public float damage = 20f;
    public float firerate = 3f;
    public float spread = 0.1f;
    public float range = 999f;
    public int ammo = 10;
    public int clipSize = 10;
    public int reserveAmmo = 120;
    public int maxAmmo = 120;
    public float reloadTime = 1f;
    public AudioSource shootSound;
    public AudioSource reloadSound;
    public AudioSource shootEmptySound;
    //NOTE: paramaters such as damage and range only apply to hitscan
    //damage values for projectile weapons can be changed under the weapon's respective projectile
    //namely the ProjectileController script

    //for projectile weapons
    public Rigidbody projectile;
    public float velocity = 10f;


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
