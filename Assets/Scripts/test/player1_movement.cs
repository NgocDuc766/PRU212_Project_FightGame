using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player1_movement : MonoBehaviour
{
    [SerializeField]
    float forceX = 10;
    [SerializeField]
    float forceY = 10;

    [SerializeField]
    float movement, vertical;
    private bool isfacingright = true;
    private bool isGround, hit, kick, superHit, superKick, jump;


    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    Rigidbody2D rigit;
    Animator playeranimator;

    //public HP hp;
    public float currentHP;
    public float maxHP = 20;

    void Start()
    {
        rigit = gameObject.GetComponent<Rigidbody2D>();
        playeranimator = GetComponent<Animator>();
        //dashanimator = GetComponentInChildren<Animator>();
        currentHP = maxHP;
        //hp.updateHP(currentHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rigit.velocity = new Vector2(forceX * movement, rigit.velocity.y);
        Vector3 CharacterScale = transform.localScale;

        if (isfacingright == true && movement == -1)
        {
            transform.localScale = new Vector3(-Mathf.Abs(CharacterScale.x), CharacterScale.y, 1);
            isfacingright = false;
        }
        if (isfacingright == false && movement == 1)
        {
            transform.localScale = new Vector3(Mathf.Abs(CharacterScale.x), CharacterScale.y, 1);
            isfacingright = true;
        }

        if (movement == 0)
        {
            playeranimator.SetInteger("speedX", 0);
            //dashanimator.SetBool("dash_effect", false);

        }
        else
        {
            playeranimator.SetInteger("speedX", 1);
            //dashanimator.SetBool("dash_effect", true);
        }

        if (vertical > 0.1f && isGround)
        {
            jump = true;
        }
        if (jump)
        {
            Jump(forceY);
            jump = false;
        }

        //if (Input.GetKeyUp(KeyCode.Space) && isjump)
        //{
        //    rigit.AddForce(Vector2.up * forceY, ForceMode2D.Impulse);
        //    playeranimator.SetBool("jump", true);
        //    isjump = false;
        //}

        if (Input.GetKey(KeyCode.S))
        {
            playeranimator.SetBool("coach", true);
        }
        else
        {
            playeranimator.SetBool("coach", false);

        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            playeranimator.SetTrigger("fireball");

            if (Input.GetKey(KeyCode.W))
            {
                FireProjectile(1);
            }
            else
            {
                FireProjectile(0);
            }
        }

    }

    public void Jump(float speedJump)
    {
        rigit.velocity = new Vector2(rigit.velocity.x, speedJump);
        //isGround = false;
        playeranimator.SetBool("jump", jump);
    }

    //private void OnTriggerEnter2D(Collider2D hitboxground)
    //{
    //    if (hitboxground.gameObject.CompareTag("ground"))
    //    {
    //        isjump = true;
    //        playeranimator.SetBool("jump", false);
    //    }
    //}



    private void FireProjectile(int type)
    {
        // Tạo ra viên đạn từ vị trí firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        if (type == 0)
        {
            if (isfacingright)
            {
                rbProjectile.velocity = new Vector2(projectileSpeed, 0); // Bắn về phía phải
            }
            else
            {
                rbProjectile.velocity = new Vector2(-projectileSpeed, 0); // Bắn về phía trái
                rbProjectile.transform.localScale = new Vector3(-projectile.transform.localScale.x, projectile.transform.localScale.y, projectile.transform.localScale.z); // Đặt scale X là -1 cho hướng trái

            }
        }
        else if (type == 1)
        {
            rbProjectile.velocity = new Vector2(isfacingright ? projectileSpeed : -projectileSpeed, projectileSpeed);
        }

    }

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("groundenemy"))
        //    {
        //        currentHP -= 2;
        //        hp.updateHP(currentHP, maxHP);
        //        collision.gameObject.SetActive(false);
        //    }
        //    else if (collision.gameObject.CompareTag("flyenemy"))
        //    {
        //        currentHP -= 4;
        //        hp.updateHP(currentHP, maxHP);
        //        collision.gameObject.SetActive(false);
        //    }

        //    if (currentHP > 0)
        //    {
        //        playeranimator.SetBool("die", false);
        //        //Destroy(gameObject);
        //    }
        //    else
        //    {
        //        playeranimator.SetBool("die", true);
        //        //FindObjectOfType<Controller_UI>().gameOver();

        //    }
        //}

}
