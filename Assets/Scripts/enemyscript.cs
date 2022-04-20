using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyscript : MonoBehaviour
{

    public float speed = 5f;             //Speed variable
    public Transform target;
    public float Range;
    bool isTarget = false;
    public float fireRate;
    float nextTimeToFire = 0;
    public float HP;
    public float HPmax;
    public float bulletForce;

    public Transform enemyFirePoint;
    public GameObject bulletPrefab;
    public GameObject HPUI;
    public Slider slider;

    void Start()
    {
        HP = HPmax;
        slider.value = CalculateHP();
    }

    void FindPlayer()
    {

        target = GameObject.Find("player(Clone)").transform;
        if(target != null)
        {
            isTarget = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        slider.value = CalculateHP();
        if(isTarget==false)
        {
            FindPlayer();
        }
        else
        {
            Vector2 direction = target.position - transform.position;
            if (direction.sqrMagnitude <= Range)
            {
              float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            if(Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / fireRate;
                    Shoot();
                }
            }
            if(HP > HPmax)
            {
                HP = HPmax;
            }

            if(HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    float CalculateHP()
    {
        return HP/HPmax;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            HP -= 1f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(enemyFirePoint.right * bulletForce, ForceMode2D.Impulse);

    }
}
