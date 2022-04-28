using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    playerscript PlayerScript;
    [SerializeField] GameObject player;

    void Awake()//koige esimesena yhendatakse karakterikood antud koodiga
    {
        PlayerScript = player.GetComponent<playerscript>();
    }
    // Update is called once per frame
    void Update()//iga kaader kontrollitakse, kas tulistamise nupu vajutatakse ning kui jah siis karakter tulistab
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()//funktsioon tulistamiseks
    {
        if(PlayerScript.canMove==true)//tulistada saab ainult, kui karakter elus on
        {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);//kuuli loomine vintraua ees
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();//kuulile rb2d omaduste andmine
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        }
    }
}
