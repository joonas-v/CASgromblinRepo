using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyWeaponController : MonoBehaviour
{
    //initialize vars
    public GameObject plrCamera;
    public LayerMask raycastIgnoreMask;
    public bool isSwitching = false;
    public bool canFire = true;

    //current weapon stuff
    public GameObject currentWep;
    private Vector3 currentWepPosition;
    private Quaternion currentWepRotation;
    private WepCont wepScript;
    private float nextTimeToFire = 0f;

    //target weapon stuff
    public GameObject targetWep;
    private Vector3 targetWepPosition;
    private Quaternion targetWepRotation;

    //looker variables
    public string lookerHit = "Nothing";
    private GameObject lookerGameObject;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialize transform of current weapon
        currentWepPosition = currentWep.transform.position;
        currentWepRotation = currentWep.transform.rotation;
        wepScript = currentWep.GetComponent<WepCont>();
    }

    // Update is called once per frame
    void Update()
    {
        Looker();
        //print(lookerHit); //debug

        if(lookerHit.Contains("wep_") && Input.GetKeyDown(KeyCode.F))
        {
            PickUpWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextTimeToFire)
        {
            Attack();
        }
    }

    void Looker()
    {
        RaycastHit Lhit;
        Physics.Raycast(plrCamera.transform.position, plrCamera.transform.forward, out Lhit, 3f, ~raycastIgnoreMask);
        if(Lhit.transform != null)
        {
            lookerHit = Lhit.transform.name;
            lookerGameObject = Lhit.transform.gameObject;
        }
    }

    void PickUpWeapon()
    {
        //set target wep
        targetWep = lookerGameObject;
        //store transform
        targetWepPosition = targetWep.transform.position;
        targetWepRotation = targetWep.transform.rotation;
        //place target at current and disable collisions
        targetWep.transform.position = currentWep.transform.position;
        targetWep.transform.rotation = currentWep.transform.rotation;
        targetWep.GetComponent<Collider>().enabled = false;
        //place current at old target pos and enable collisions
        currentWep.transform.position = targetWepPosition;
        currentWep.transform.rotation = targetWepRotation;
        currentWep.GetComponent<Collider>().enabled = true;
        //REMOVE THE CHILD
        Transform childToRemove = currentWep.transform;
        childToRemove.parent = null;
        Transform childToReplace = targetWep.transform;
        childToReplace.parent = gameObject.transform;
        currentWep = targetWep;
        //get script
        wepScript = currentWep.GetComponent<WepCont>();
    }

    void Attack()
    {
        if (wepScript.hitScan)
        {
            //s p r e a d
            Vector3 spread = plrCamera.transform.forward;
            spread.x += UnityEngine.Random.Range(wepScript.spread, wepScript.spread);
            spread.y += UnityEngine.Random.Range(wepScript.spread, wepScript.spread);

            //raycasting
            RaycastHit hit;
            Physics.Raycast(plrCamera.transform.position, spread, out hit, 999f, ~raycastIgnoreMask);

            //dealing damage
            if (hit.transform != null)
            {
                if (hit.transform.name.Contains("Enemy"))
                {
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                    enemy.TakeDamage(wepScript.damage);
                }
            }

            //fire rate
            nextTimeToFire = Time.time + 1 / wepScript.firerate;
        }
    }
}
