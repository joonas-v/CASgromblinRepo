using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiProjAimer : MonoBehaviour
{
    public Rigidbody projectile;
    private GameObject player;
    private float nextTimeToFire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(player.transform);
        if(Time.time > nextTimeToFire)
        {
            projectile = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
            projectile.velocity = transform.TransformDirection(Vector3.forward * 40f);
            nextTimeToFire = Time.time + 2f;
        }
    }
}
