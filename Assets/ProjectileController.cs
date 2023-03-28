using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //initialize vars
    public float damage = 35f;

    //UI stuff (mostly for damage values)
    public GameObject plrUI;
    private UIController ui;

    // Start is called before the first frame update
    void Start()
    {
        plrUI = GameObject.Find("UI");
        ui = plrUI.GetComponent<UIController>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player") | collision.gameObject.name.Contains("wep_"))
        {
            Collider collider = gameObject.GetComponent<Collider>();
            Physics.IgnoreCollision(collision.collider, collider);
        }
        else if (collision.transform.name.Contains("Enemy"))
        {
            EnemyController enemy = collision.transform.GetComponent<EnemyController>();
            if (!enemy.isDead)
            {
                enemy.TakeDamage(damage);
                ui.HitText((-damage).ToString());
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
