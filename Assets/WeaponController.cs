using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Initialize variables
    public GameObject plrCamera;
    public LayerMask raycastIgnoreMask;
    public int equippedWep;
    public bool isSwitching = false;
    public bool canFire = true;
    public float wepSwitchTime = 0.75f;

    public GameObject testgun;
    public GameObject testgunTip;
    private TestGunController testgunScript;
    private float nextTimeToFireTestGun;
    private AudioSource shoot;


    private GameObject currentWeapon;



    // Start is called before the first frame update
    void Start()
    {
        testgunScript = testgun.GetComponent<TestGunController>();
        shoot = testgun.GetComponent<AudioSource>();
        currentWeapon = testgun;
        equippedWep = 1;
        Renderer[] allguns = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in allguns)
        {
            rend.enabled = false;
        }
        currentWeapon.GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextTimeToFireTestGun && canFire == true)
        {
            FireTestGun();
        }
    }

    void FireTestGun()
    {
        //s p r e a d
        Vector3 spread = plrCamera.transform.forward;
        spread.x += UnityEngine.Random.Range(-testgunScript.spread, testgunScript.spread);
        spread.y += UnityEngine.Random.Range(-testgunScript.spread, testgunScript.spread);

        //raycasting
        RaycastHit hit;

        Physics.Raycast(plrCamera.transform.position, spread, out hit, 999f, ~raycastIgnoreMask);

        //adding force to hit rb
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(plrCamera.transform.forward * testgunScript.impactForce, ForceMode.Impulse);
            //Debug.Log(hit.distance);
        }

        //dealing damage
        if (hit.transform != null)
        {
            if (hit.transform.name.Contains("Enemy"))
            {
                EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                enemy.TakeDamage(testgunScript.damage);
            }
        }

        //fire rate
        nextTimeToFireTestGun = Time.time + 1 / testgunScript.firerate;

        //muzzle flash
        Instantiate(testgunScript.muzzleFlash, testgunTip.transform.position, testgunTip.transform.rotation);

        //fire sound
        shoot.PlayOneShot(shoot.clip);
    }
}
