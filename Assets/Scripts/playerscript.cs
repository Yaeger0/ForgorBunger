using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerscript : MonoBehaviour
{
    private Rigidbody2D rb2d;       //Reference to the Rigidbody2D component
    public float speed;        //Speed variable
    public float aimspeed;
    public int currentHealth = 0;
    public int maxHealth = 100;
    public HealthBar healthBar;
    public bool canMove = true;

    [SerializeField] private Text gameOver;


    // Start is called before the first frame update
    void Start()
    {
        //Makes the Rigidbody2D component accessible
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove==true)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            //This supplies movement to the player
            rb2d.AddForce(movement * speed);
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, aimspeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            DamagePlayer(10);
        }

    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            canMove = false;
            GameOver();
        }
        else
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
    }
}
