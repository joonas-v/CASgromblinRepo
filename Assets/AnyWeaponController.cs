using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public WepCont wepScript;
    private float nextTimeToFire = 0f;
    public bool reloading = false;

    //target weapon stuff
    public GameObject targetWep;
    private Vector3 targetWepPosition;
    private Quaternion targetWepRotation;

    //looker variables
    public string lookerHit = "Nothing";
    private GameObject lookerGameObject;

    //UI stuff
    public GameObject plrUI;
    private UIController ui;

    //projectile stuff
    public GameObject projectileSpawn;

    //player stuff
    private PlrController player;

    // Start is called before the first frame update
    void Start()
    {
        //initialize transform of current weapon
        currentWepPosition = currentWep.transform.position;
        currentWepRotation = currentWep.transform.rotation;
        wepScript = currentWep.GetComponent<WepCont>();
        ui = plrUI.GetComponent<UIController>();
        currentWep.GetComponent<Collider>().enabled = false;
        player = GameObject.Find("Player").GetComponent<PlrController>();
    }

    // Update is called once per frame
    void Update()
    {
        Looker();
        //print(lookerHit); //debug
        if (!player.isDead)
        {
            if (lookerHit.Contains("wep_") && Input.GetKeyDown(KeyCode.F) && !reloading)
            {
                PickUpWeapon();
            }
            //dont ask why this is here lol
            else if (lookerHit.Contains("FinishButton") && Input.GetKeyDown(KeyCode.F))
            {
                ProceduralGenController room = GameObject.Find("ProceduralGenerator").GetComponent<ProceduralGenController>();
                if (room.roomObjectiveDone)
                {
                    room.RoomFinish(room.currentRoom);
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextTimeToFire)
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.R) && !reloading)
            {
                Reload();
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                SceneManager.LoadScene("SampleScene");
            }
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
        if (wepScript.ammo > 0 && !reloading)
        {
            if (wepScript.hitScan)
            {
                //take ammo
                wepScript.ammo -= 1;

                //play sound
                wepScript.shootSound.PlayOneShot(wepScript.shootSound.clip);

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
                        if (enemy.isDead == false)
                        {
                            enemy.TakeDamage(wepScript.damage);
                            ui.HitText((-wepScript.damage).ToString());
                        }
                    }
                }

                //fire rate
                nextTimeToFire = Time.time + 1 / wepScript.firerate;
            }
            else if (!wepScript.hitScan)
            {
                //take ammo
                wepScript.ammo -= 1;

                //play sound
                wepScript.shootSound.PlayOneShot(wepScript.shootSound.clip);

                //s p r e a d
                Vector3 spread = plrCamera.transform.forward;
                spread.x += UnityEngine.Random.Range(wepScript.spread, wepScript.spread);
                spread.y += UnityEngine.Random.Range(wepScript.spread, wepScript.spread);

                //instantiate projectile
                Rigidbody projRb;
                projRb = Instantiate(wepScript.projectile, projectileSpawn.transform.position, plrCamera.transform.rotation);
                projRb.velocity = transform.TransformDirection(Vector3.forward * wepScript.velocity);

                //fire rate
                nextTimeToFire = Time.time + 1 / wepScript.firerate;
            }
        }
        else
        {
            wepScript.shootEmptySound.PlayOneShot(wepScript.shootEmptySound.clip);
        }
    }

    void Reload()
    {
        int ammoToReload = wepScript.clipSize - wepScript.ammo;
        if (wepScript.reserveAmmo != 0 && wepScript.ammo != wepScript.clipSize)
        {
            StartCoroutine(Reloader(wepScript.reloadTime));
        }
    }

    IEnumerator Reloader(float seconds)
    {
        ui.reloading.text = "Reloading...";
        reloading = true;
        yield return new WaitForSeconds(seconds);
        if(wepScript.clipSize - wepScript.ammo <= wepScript.reserveAmmo)
        {
            wepScript.reserveAmmo -= (wepScript.clipSize - wepScript.ammo);
            wepScript.ammo = wepScript.clipSize;
        }
        else if (wepScript.clipSize - wepScript.ammo > wepScript.reserveAmmo)
        {
            wepScript.ammo += wepScript.reserveAmmo;
            wepScript.reserveAmmo = 0;
        }
        reloading = false;
        ui.reloading.text = "";
        wepScript.reloadSound.PlayOneShot(wepScript.reloadSound.clip);
    }
}
