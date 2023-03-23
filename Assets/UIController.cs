using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject healthBar;
    public Slider healthSlider;
    public PlrController player;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = healthBar.GetComponent<Slider>();
        player = GetComponentInParent<PlrController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = player.health / player.maxHealth;
    }
}
