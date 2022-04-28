using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public playerscript playerHealth;

    // Start is called before the first frame update
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerscript>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;//seab sliderile vaartuse hp, elude koguse
    }
}
