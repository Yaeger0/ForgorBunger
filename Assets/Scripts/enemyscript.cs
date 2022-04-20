using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{

    public float speed = 5f;             //Speed variable
    public Transform target;
    public float Range;
    bool isTarget = false;
    public float fireRate;
    float nextTimeToFire = 0;
    public static float healthAmount;

    public Transform enemyFirePoint;
    public GameObject bulletPrefab;
    public float bulletForce;

    void Start()
    {
        healthAmount = 1;
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
            if(healthAmount <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            healthAmount -= 0.1f;
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
