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

    void Awake()
    {
        PlayerScript = player.GetComponent<playerscript>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(PlayerScript.canMove==true)
        {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        }
    }
}
