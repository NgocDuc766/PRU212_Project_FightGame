using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float forceX, forceY;
    Rigidbody2D rigit;
    Animator animator;
    float jumpPower = 9.5f;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rigit = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("StateIndex", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
            );
            rigit.AddForce(forceX * Vector2.right);
            animator.SetInteger("StateIndex", 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(
            -Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
            );
            rigit.AddForce(forceX * Vector2.left);
            animator.SetInteger("StateIndex", 1);
        }


        // code de nhay
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigit.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isGrounded = false; // Nhân vật không còn trên mặt đất
            animator.SetBool("isJumping", !isGrounded); // Bắt đầu animation nhảy
        }

        if (rigit.velocity.x == 0)
        {
            animator.SetInteger("StateIndex", 0);
        }

        // neu nhan vat dang roi
        if (rigit.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("isFalling", true); // Kích hoạt animation rơi
            // tắt animation nhảy
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isFalling", false); // Tắt animation rơi
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Fire");
        }

        if (Input.GetKey(KeyCode.X))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Defend");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //UIControllerScript ui = FindObjectOfType<UIControllerScript>();
        //if (collision.gameObject.CompareTag("Enemy1"))
        //{
        //    ui.TakeDamageFromEnemy(5);
        //    collision.gameObject.SetActive(false);
        //}
        //if (collision.gameObject.CompareTag("Enemy2"))
        //{
        //    ui.TakeDamageFromEnemy(10);
        //    collision.gameObject.SetActive(false);
        //}
    }
}
