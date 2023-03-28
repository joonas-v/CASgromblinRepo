using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    //necessary script gameobject reference
    public GameObject weaponHolder;
    private AnyWeaponController weapon;
    //all the fuckin ui elements
    public GameObject healthBar;
    private Slider healthSlider;
    public PlrController player;
    public GameObject damageText;
    private TMP_Text damage;
    public GameObject healthText;
    private TMP_Text health;
    public GameObject ammoText;
    private TMP_Text ammo;
    public GameObject reloadText;
    public TMP_Text reloading;
    public GameObject healthPotText;
    public TMP_Text healthPot;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = healthBar.GetComponent<Slider>();
        player = GetComponentInParent<PlrController>();
        damage = damageText.GetComponent<TMP_Text>();
        damage.text = "";
        health = healthText.GetComponent<TMP_Text>();
        ammo = ammoText.GetComponent<TMP_Text>();
        weapon = weaponHolder.GetComponent<AnyWeaponController>();
        reloading = reloadText.GetComponent<TMP_Text>();
        reloading.text = "";
        healthPot = healthPotText.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = player.health / player.maxHealth;
        health.text = player.health.ToString() + " / " + player.maxHealth.ToString();
        ammo.text = weapon.wepScript.ammo.ToString() + " / " + weapon.wepScript.reserveAmmo.ToString();
        if(weapon.wepScript.ammo == 0 && weapon.wepScript.ammo == weapon.wepScript.reserveAmmo)
        {
            reloading.text = "Out of ammo!";
        }
        else if(!weapon.reloading)
        {
            reloading.text = "";
        }
        healthPot.text = "Health potions: " + player.healthPotions;
    }

    public void HitText(string msg)
    {
        damage.CrossFadeAlpha(1f, 0f, false);
        damage.text = msg;
        damage.CrossFadeAlpha(0f, 1f, false);
    }
}
